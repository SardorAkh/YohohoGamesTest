using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Systems
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsPoolInject<UnitCmp> _unitCmpPool;
        private readonly EcsPoolInject<PlayerTag> _playerTagPool;
        private readonly EcsCustomInject<SceneService> _sceneService;
        
        private int _playerEntity;

        public void Init(IEcsSystems systems)
        {
            _playerEntity = _defaultWorld.Value.NewEntity();

            _playerTagPool.Value.Add(_playerEntity);
            ref var playerCmp = ref _unitCmpPool.Value.Add(_playerEntity);

            playerCmp.View = _sceneService.Value.PlayerView;
            playerCmp.Speed = _sceneService.Value.PlayerMoveSpeed;
        }

        public void Run(IEcsSystems systems)
        {
            if (!_unitCmpPool.Value.Has(_playerEntity)) return;
            var direction = new Vector3(_sceneService.Value.JoystickController.Horizontal(), 0,
                _sceneService.Value.JoystickController.Vertical());

            ref var playerCmp = ref _unitCmpPool.Value.Get(_playerEntity);
            playerCmp.Direction = direction;
        }
    }
}