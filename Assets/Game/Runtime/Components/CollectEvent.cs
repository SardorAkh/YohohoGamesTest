using Leopotam.EcsLite;
using Runtime.Views;

namespace Runtime.Components
{
    public struct CollectEvent
    {
        public EcsPackedEntityWithWorld CharacterEntity;
        public EcsPackedEntityWithWorld SupplyStationEntity;
    }
}