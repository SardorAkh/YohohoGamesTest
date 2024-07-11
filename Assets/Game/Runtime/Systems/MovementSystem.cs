using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using UnityEngine;

namespace Runtime.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsFilterInject<Inc<UnitCmp>> _unitCmpFilter;

        public void Run(IEcsSystems systems)
        {
            //Бежим по всем сущностям с UnitCmp
            foreach (var entity in _unitCmpFilter.Value)
            {
                //Получаем текущую скорость и отображение
                var unitCmp = _unitCmpPool.Value.Get(entity);
                var direction = unitCmp.Direction;
                var speed = unitCmp.Speed;
                var view = unitCmp.View;


                if (direction == Vector3.zero)
                    continue;

                // Двигаем отображение
                var translation = direction * (speed * Time.deltaTime);
                view.Move(translation);
            }
        }
    }
}