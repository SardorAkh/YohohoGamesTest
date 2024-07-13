using System;
using Leopotam.EcsLite;
using Runtime.Components;
using UnityEngine;

namespace Runtime.Views
{
    public class CharacterView : MonoBehaviour
    {
        [field:SerializeField] public float MoveSpeed { get; private set; }
        [field:SerializeField] public int MaxCarryCapacity { get; private set; }
        [field: SerializeField] public Transform ItemHoldPosition { get; private set; }
        
        public EcsPackedEntityWithWorld PackedEntity { get; private set; }
        private EcsWorld _eventEcsWorld;

        private int _lastCollectEventId;
        private int _lastDropEventId;
        
        public void Construct(EcsPackedEntityWithWorld entity, EcsWorld eventEcsWorld)
        {
            PackedEntity = entity;
            _eventEcsWorld = eventEcsWorld;
        }
        public void Move(Vector3 desired)
        {
            transform.position += desired;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SupplyStationView supplyStationView))
            {
                var entity = _eventEcsWorld.NewEntity();
                ref var collectEvent = ref _eventEcsWorld.GetPool<CollectEvent>().Add(entity);
                collectEvent.From = supplyStationView.PackedEntity;
                collectEvent.To = PackedEntity;

                _lastCollectEventId = entity;
            }

            if (other.TryGetComponent(out SupplyDepotStationView supplyDepotStationView))
            {
                var entity = _eventEcsWorld.NewEntity();
                ref var collectEvent = ref _eventEcsWorld.GetPool<CollectEvent>().Add(entity);
                collectEvent.From = PackedEntity;
                collectEvent.To = supplyDepotStationView.PackedEntity;

                _lastDropEventId = entity;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SupplyStationView supplyStationView))
            {
                _eventEcsWorld.GetPool<CollectEvent>().Del(_lastCollectEventId);
            }

            if (other.TryGetComponent(out SupplyDepotStationView supplyDepotStationView))
            {
                _eventEcsWorld.GetPool<CollectEvent>().Del(_lastDropEventId);
            }
        }
    }
}