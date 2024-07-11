using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using UnityEngine;

namespace Client.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsFilterInject<Inc<UnitCmp>> _unitCmpFilter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _unitCmpFilter.Value)
            {
                var unitCmp = _unitCmpPool.Value.Get(entity);
                var velociy = unitCmp.Velocity;
                var view = unitCmp.View;
                
                if(velociy == Vector3.zero)
                    continue;

                var translation = velociy * Time.deltaTime;
                view.Move(translation);
            }    
        }
    }
}