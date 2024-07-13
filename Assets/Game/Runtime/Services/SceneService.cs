using System.Collections.Generic;
using Runtime.Tools;
using Runtime.Views;
using UnityEngine;

namespace Runtime.Services
{
    public class SceneService : MonoBehaviour
    {
        [field: SerializeField] public JoystickController JoystickController { get; private set; }
        [field: SerializeField] public List<SupplyStationView> SupplyStationViews { get; private set; }
        [field: SerializeField] public List<SupplyDepotStationView> SupplyDepotStationViews { get; private set; }
    }
}
