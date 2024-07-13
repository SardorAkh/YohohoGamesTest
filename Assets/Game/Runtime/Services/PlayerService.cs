using Leopotam.EcsLite;
using Runtime.Views;
using TMPro;
using UnityEngine;

namespace Runtime.Services
{
    public class PlayerService : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text _stackText;
        [field: SerializeField] public CharacterView PlayerView { get; private set; }
        public EcsPackedEntityWithWorld Player { get; set; }
    }
}