using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventsWorld = Constants.EventWorldName;
        private readonly EcsPoolInject<InputEvent> _inputPool = Constants.EventWorldName;
        private readonly EcsCustomInject<SceneService> _sceneService;

        public void Run(IEcsSystems systems)
        {
            var direction = new Vector3(_sceneService.Value.JoystickController.Horizontal(), 0,
                _sceneService.Value.JoystickController.Vertical());

            if (direction == Vector3.zero) return;

            var entity = _eventsWorld.Value.NewEntity();
            ref var eventCmp = ref _inputPool.Value.Add(entity);
            eventCmp.Horizontal = _sceneService.Value.JoystickController.Horizontal();
            eventCmp.Vertical = _sceneService.Value.JoystickController.Vertical();
        }
    }
}