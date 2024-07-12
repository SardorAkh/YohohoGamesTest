using System;
using Leopotam.EcsLite;
using Runtime.Components;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Views
{
    public class SupplyStationView : MonoBehaviour
    {
        [SerializeField] private ItemView _supplyItem;
        [SerializeField] private ChildCollider _childCollider;
        [field: SerializeField] public int MaxCarryCapacity { get; private set; }
        [field: SerializeField] public Transform ItemHoldPosition { get; private set; }
        
        private EcsWorld _eventEcsWorld;
        private int _lastEventEntity;

        public void Construct(EcsWorld eventEcsWorld)
        {
            _eventEcsWorld = eventEcsWorld;
        }
        
        private void OnEnable()
        {
            _childCollider.OnChildTriggerEnter += ChildColliderOnChildTriggerEnter;
            _childCollider.OnChildTriggerExit += ChildColliderOnChildTriggerExit;
        }


        private void OnDisable()
        {
            _childCollider.OnChildTriggerEnter -= ChildColliderOnChildTriggerEnter;
            _childCollider.OnChildTriggerExit -= ChildColliderOnChildTriggerExit;
        }

        private void ChildColliderOnChildTriggerEnter(Collider obj)
        {
            if (obj.TryGetComponent(out CharacterView characterView))
            {
                var entity = _eventEcsWorld.NewEntity();
                ref var collectEvent = ref _eventEcsWorld.GetPool<CollectEvent>().Add(entity);
                collectEvent.CharacterView = characterView;
                collectEvent.ItemView = _supplyItem;

                _lastEventEntity = entity;
            }
        }

        private void ChildColliderOnChildTriggerExit(Collider obj)
        {
            if (obj.TryGetComponent(out CharacterView characterView))
            {
                _eventEcsWorld.GetPool<CollectEvent>().Del(_lastEventEntity);
            }
        }
    }
}