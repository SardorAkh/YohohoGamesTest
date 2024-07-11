using Runtime.Tools;
using Runtime.Views;
using UnityEngine;

namespace Runtime.Services
{
    public class SceneService : MonoBehaviour
    {
        [field: SerializeField] public UnitView PlayerView { get; private set; }
        [field: SerializeField] public float PlayerMoveSpeed { get; private set; }
        [field: SerializeField] public JoystickController JoystickController { get; private set; }
    }
}
