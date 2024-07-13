using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;
using Runtime.Tools;
using Runtime.Views;

namespace Runtime.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<PlayerService> _playerService;
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventWorld = Constants.EventWorldName;
        
        public void Init(IEcsSystems systems)
        {
            var entity = _defaultWorld.Value.NewEntity();
            
            var packedEntity = _defaultWorld.Value.PackEntityWithWorld(entity);
            _playerService.Value.Player = packedEntity;
            var playerView = _playerService.Value.PlayerView;
            
            ref var characterCmp = ref _defaultWorld.Value.GetPool<CharacterCmp>().Add(entity);
            characterCmp.CharacterView = playerView;
            characterCmp.CharacterView.Construct(packedEntity, _eventWorld.Value);

            ref var itemsStackCmp = ref _defaultWorld.Value.GetPool<ItemStackCmp>().Add(entity);
            itemsStackCmp.ItemsStack = new Stack<ItemView>();
            itemsStackCmp.MaxCapacity = playerView.MaxCarryCapacity;
            itemsStackCmp.CarryingPointTransform = playerView.ItemHoldPosition;
        }
    }
}