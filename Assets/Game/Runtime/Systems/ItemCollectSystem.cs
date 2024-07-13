using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Systems
{
    public class ItemCollectSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollectEvent>> _collectEventFilter = Constants.EventWorldName;
        private readonly EcsPoolInject<CharacterCmp> _characterPool;
        private readonly EcsPoolInject<SupplyStationCmp> _supplyStationPool;
        
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _collectEventFilter.Value)
            {
                var pool = _collectEventFilter.Pools.Inc1;
                var collectEvent = pool.Get(entity);

                var player = _characterPool.Value.Get(collectEvent.CharacterEntity.Id);
                var supplyStation = _supplyStationPool.Value.Get(collectEvent.SupplyStationEntity.Id);

                if (supplyStation.ItemsStack.Count == 0) return;
                
                if (player.ItemsStack.Count < player.CharacterView.MaxCarryCapacity)
                {
                    var item = supplyStation.ItemsStack.Pop();
                    player.ItemsStack.Push(item);
                    item.transform.parent = player.CharacterView.ItemHoldPosition;
                    item.transform.localPosition = new Vector3(0, player.ItemsStack.Count * 0.5f, 0);
                }                
            }
        }
    }
}