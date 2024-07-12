using System.Collections.Generic;
using Runtime.Configs;
using Runtime.Views;

namespace Runtime.Components
{
    public struct CharacterCmp
    {
        public CharacterView CharacterView;
        public Stack<CollectibleItemConfig> ItemsStack;
    }
}