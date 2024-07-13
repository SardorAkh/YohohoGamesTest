using System.Collections.Generic;
using Runtime.Views;
using UnityEngine;

namespace Runtime.Components
{
    public struct ItemStackCmp
    {
        public Stack<ItemView> ItemsStack;
        public int MaxCapacity;
        public Transform CarryingPointTransform;
    }
}