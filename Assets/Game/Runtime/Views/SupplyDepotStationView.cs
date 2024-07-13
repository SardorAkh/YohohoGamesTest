using System;
using Leopotam.EcsLite;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Views
{
    public class SupplyDepotStationView : MonoBehaviour
    {
        [field: SerializeField] public ItemView SupplyItem { get; private set; }
        [field: SerializeField] public int MaxCarryCapacity { get; private set; }
        [field: SerializeField] public Transform ItemPlacePosition { get; private set; }

        public EcsPackedEntityWithWorld PackedEntity;
        
        public void Construct( EcsPackedEntityWithWorld entity)
        {
            PackedEntity = entity;
        }
    }
}