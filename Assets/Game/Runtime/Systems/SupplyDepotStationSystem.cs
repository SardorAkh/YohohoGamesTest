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
    public class SupplyDepotStationSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsPoolInject<SupplyDepotStationCmp> _supplyDepotStationPool;
        private readonly EcsPoolInject<ItemStackCmp> _itemStackPool;
        private readonly EcsFilterInject<Inc<SupplyDepotStationCmp>> _supplyDepotStationFilter;
        private readonly EcsCustomInject<SceneService> _sceneService;

        public void Init(IEcsSystems systems)
        {
            foreach (var supplyDepotStation in _sceneService.Value.SupplyDepotStationViews)
            {
                var entity = _defaultWorld.Value.NewEntity();
                
                ref var supplyDepotStationCmp = ref _supplyDepotStationPool.Value.Add(entity);
                supplyDepotStationCmp.SupplyDepotStationView = supplyDepotStation;
                supplyDepotStationCmp.SupplyDepotStationView.Construct(_defaultWorld.Value.PackEntityWithWorld(entity));

                ref var itemStackCmp = ref _defaultWorld.Value.GetPool<ItemStackCmp>().Add(entity);
                itemStackCmp.ItemsStack = new();
                itemStackCmp.MaxCapacity = supplyDepotStation.MaxCarryCapacity;
                itemStackCmp.CarryingPointTransform = supplyDepotStation.ItemPlacePosition;
            }
        }
    }
}