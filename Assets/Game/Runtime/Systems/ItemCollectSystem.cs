using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Systems
{
    public class ItemCollectSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollectEvent>> _collectEventFilter = EcsKeys.EventWorldName;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _collectEventFilter.Value)
            {
                Debug.Log(entity);
            }
        }
    }
}