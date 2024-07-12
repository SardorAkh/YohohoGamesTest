using UnityEngine;

namespace Runtime.Configs
{
    [CreateAssetMenu(fileName = nameof(CollectibleItemConfig), menuName = nameof(Runtime) + "/" + nameof(CollectibleItemConfig),
        order = 0)]
    public class CollectibleItemConfig : ScriptableObject
    {
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}