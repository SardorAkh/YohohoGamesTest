using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Components;
using Runtime.Services;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputEvent>> _inputFilter = EcsKeys.EventWorldName;
        private readonly EcsCustomInject<PlayerService> _playerService;
        private readonly EcsWorldInject _defaultWorld = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _inputFilter.Value)
            {
                var pool = _inputFilter.Pools.Inc1;

                if (!pool.Has(entity))
                {
                    continue;
                }

                var cmp = pool.Get(entity);
                pool.Del(entity);

                var playerId = _playerService.Value.Player.Id;
                var player = _defaultWorld.Value.GetPool<CharacterCmp>().Get(playerId);

                var desired = new Vector3(cmp.Horizontal, 0, cmp.Vertical) * player.CharacterView.MoveSpeed *
                              Time.deltaTime;

                player.CharacterView.Move(desired);
            }
        }
    }
}