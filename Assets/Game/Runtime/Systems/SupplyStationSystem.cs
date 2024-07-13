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
        private readonly EcsFilterInject<Inc<SupplyStationCmp>> _supplyStationFilter;
        private readonly EcsCustomInject<SceneService> _sceneService;

        public void Init(IEcsSystems systems)
        {
            foreach (var supplyStation in _sceneService.Value.SupplyStationViews)
            {
                var entity = _defaultWorld.Value.NewEntity();
                ref var supplyStationCmp = ref _supplyStationPool.Value.Add(entity);
                supplyStationCmp.SupplyStationView = supplyStation;
                supplyStationCmp.ItemsStack = new Stack<ItemView>();
                supplyStationCmp.SpawnInterval = 2f;
                supplyStationCmp.SpawnTimer = 0f;
                supplyStationCmp.SupplyStationView.Construct(_eventWorld.Value,
                    _defaultWorld.Value.PackEntityWithWorld(entity));
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _supplyStationFilter.Value)
            {
                ref var supplyStationCmp = ref _supplyStationPool.Value.Get(entity);
                var itemsStack = supplyStationCmp.ItemsStack;
                var supplyStationView = supplyStationCmp.SupplyStationView;
                var stackCount = itemsStack.Count;
                var maxStackCount = supplyStationView.MaxCarryCapacity;

                if (stackCount >= maxStackCount) return;

                supplyStationCmp.SpawnTimer += Time.deltaTime;

                if (supplyStationCmp.SpawnTimer < supplyStationCmp.SpawnInterval) return;

                supplyStationCmp.SpawnTimer = 0;


                var itemView = Object.Instantiate(supplyStationView.SupplyItem, supplyStationView.ItemHoldPosition);
                itemView.transform.localPosition = new Vector3(0, stackCount * .5f, 0);
                itemsStack.Push(itemView);
            }
        }
    }
}