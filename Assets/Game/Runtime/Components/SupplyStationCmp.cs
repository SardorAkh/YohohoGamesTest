using System.Collections.Generic;
using Runtime.Views;

namespace Runtime.Components
{
    public struct SupplyStationCmp
    {
        public SupplyStationView SupplyStationView;
        public Stack<ItemView> ItemsStack;
        public float SpawnInterval;
        public float SpawnTimer;
    }
}