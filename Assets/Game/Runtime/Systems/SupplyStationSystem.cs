using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Configs;
using Runtime.Services;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Systems
{
    public class SupplyStationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventWorld = EcsKeys.EventWorldName;
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
                supplyStationCmp.ItemsStack = new Stack<CollectibleItemConfig>();
                supplyStationCmp.SpawnInterval = 2f;
                supplyStationCmp.SpawnTimer = 0f;
                supplyStationCmp.SupplyStationView.Construct(_eventWorld.Value);
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _supplyStationFilter.Value)
            {
                ref var supplyStationCmp = ref _supplyStationPool.Value.Get(entity);
                
                supplyStationCmp.SpawnTimer += Time.deltaTime;
                
                if (supplyStationCmp.SpawnTimer < supplyStationCmp.SpawnInterval) return;
                
                supplyStationCmp.SpawnTimer = 0;
                Debug.Log("Spawn at " + supplyStationCmp.SupplyStationView.name);
            }
        }
    }
}