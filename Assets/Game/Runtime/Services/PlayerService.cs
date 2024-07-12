using Leopotam.EcsLite;
using Runtime.Views;
using UnityEngine;

namespace Runtime.Services
{
    public class PlayerService : MonoBehaviour
    {
        [field: SerializeField] public CharacterView PlayerView { get; private set; }
        public EcsPackedEntityWithWorld Player { get; set; }
    }
}