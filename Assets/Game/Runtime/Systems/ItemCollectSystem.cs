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
        private readonly EcsPoolInject<ItemStackCmp> _itemStackPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _collectEventFilter.Value)
            {
                var pool = _collectEventFilter.Pools.Inc1;
                var collectEvent = pool.Get(entity);

                var from = _itemStackPool.Value.Get(collectEvent.From.Id);
                var to = _itemStackPool.Value.Get(collectEvent.To.Id);
                

                if (from.ItemsStack.Count == 0) return;
                
                if (to.ItemsStack.Count < to.MaxCapacity)
                {
                    var item = from.ItemsStack.Pop();
                    to.ItemsStack.Push(item);
                    item.transform.parent = to.CarryingPointTransform;
                    item.transform.localPosition = new Vector3(0, to.ItemsStack.Count * 0.5f, 0);
                }                
            }
        }
    }
}