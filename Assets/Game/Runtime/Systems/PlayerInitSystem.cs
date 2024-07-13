using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;
using Runtime.Views;

namespace Runtime.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<PlayerService> _playerService;
        private readonly EcsWorldInject _defaultWorld = default;
        
        public void Init(IEcsSystems systems)
        {
            var entity = _defaultWorld.Value.NewEntity();
            var packedEntity = _defaultWorld.Value.PackEntityWithWorld(entity);
            _playerService.Value.Player = packedEntity;
            var playerView = _playerService.Value.PlayerView;
            
            ref var characterCmp = ref _defaultWorld.Value.GetPool<CharacterCmp>().Add(entity);
            characterCmp.CharacterView = playerView;
            characterCmp.CharacterView.Construct(packedEntity);
            characterCmp.ItemsStack = new Stack<ItemView>(); 
            
        }
    }
}