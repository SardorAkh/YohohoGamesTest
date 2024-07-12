using Runtime.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.Views
{
    public class ItemView : MonoBehaviour
    {
        [field: SerializeField] public CollectibleItemConfig CollectibleItemConfig { get; private set; }
        
    }
}