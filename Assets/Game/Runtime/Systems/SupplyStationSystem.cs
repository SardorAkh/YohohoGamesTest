using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;
using Runtime.Tools;
using Runtime.Views;
using UnityEngine;

namespace Runtime.Systems
{
    public class SupplyStationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventWorld = Constants.EventWorldName;
        private readonly EcsPoolInject<SupplyStationCmp> _supplyStationPool;
        private readonly EcsPoolInject<ItemStackCmp> _itemStackPool;
        private readonly EcsFilterInject<Inc<SupplyStationCmp>> _supplyStationFilter;
        private readonly EcsCustomInject<SceneService> _sceneService;

        public void Init(IEcsSystems systems)
        {
            foreach (var supplyStation in _sceneService.Value.SupplyStationViews)
            {
                var entity = _defaultWorld.Value.NewEntity();
                
                ref var supplyStationCmp = ref _supplyStationPool.Value.Add(entity);
                supplyStationCmp.SupplyStationView = supplyStation;
                supplyStationCmp.SpawnInterval = 2f;
                supplyStationCmp.SpawnTimer = 0f;
                supplyStationCmp.SupplyStationView.Construct(_defaultWorld.Value.PackEntityWithWorld(entity));

                ref var itemStackCmp = ref _defaultWorld.Value.GetPool<ItemStackCmp>().Add(entity);
                itemStackCmp.ItemsStack = new();
                itemStackCmp.MaxCapacity = supplyStation.MaxCarryCapacity;
                itemStackCmp.CarryingPointTransform = supplyStation.ItemHoldPosition;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _supplyStationFilter.Value)
            {
                ref var supplyStationCmp = ref _supplyStationPool.Value.Get(entity);
                ref var itemStackCmp = ref _itemStackPool.Value.Get(entity);
                
                var itemStack = itemStackCmp.ItemsStack;
                var maxStackCount = itemStackCmp.MaxCapacity;
                
                var supplyStationView = supplyStationCmp.SupplyStationView;

                if (itemStack.Count >= maxStackCount) return;

                supplyStationCmp.SpawnTimer += Time.deltaTime;

                if (supplyStationCmp.SpawnTimer < supplyStationCmp.SpawnInterval) return;

                supplyStationCmp.SpawnTimer = 0;

                var itemView = Object.Instantiate(supplyStationView.SupplyItem, supplyStationView.ItemHoldPosition);
                itemView.transform.localPosition = new Vector3(0, itemStack.Count * .5f, 0);
                
                itemStack.Push(itemView);
            }
        }
    }
}