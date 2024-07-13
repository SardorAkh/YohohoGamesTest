using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;

namespace Runtime.Systems
{
    public class ItemStackDisplayerSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<PlayerService> _playerService;
        private readonly EcsPoolInject<ItemStackCmp> _itemStackPool;

        public void Run(IEcsSystems systems)
        {
            var stack = _itemStackPool.Value.Get(_playerService.Value.Player.Id);
            _playerService.Value._stackText.text = String.Format(_playerService.Value._stackText.text,
                stack.ItemsStack.Count, stack.MaxCapacity);
        }
    }
}