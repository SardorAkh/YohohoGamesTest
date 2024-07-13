using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Runtime.Services;
using Runtime.Systems;
using Runtime.Tools;
using Runtime.Views;
using UnityEngine;

namespace Runtime {
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneService _sceneService;
        [SerializeField] private PlayerService _playerService;
        EcsWorld _world;        
        IEcsSystems _systems;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new ItemCollectSystem())
                .Add(new SupplyStationSystem())
                .Add(new SupplyDepotStationSystem())
                .Add(new ItemStackDisplayerSystem())
                .AddWorld (new EcsWorld (), Constants.EventWorldName)
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Inject(_sceneService, _playerService)
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}