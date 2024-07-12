using System.Collections.Generic;
using Runtime.Configs;
using Runtime.Views;

namespace Runtime.Components
{
    public struct SupplyStationCmp
    {
        public SupplyStationView SupplyStationView;
        public Stack<CollectibleItemConfig> ItemsStack;
        public float SpawnInterval;
        public float SpawnTimer;
    }
}