using Leopotam.EcsLite;
using UnityEngine;

namespace Runtime.Views
{
    public class CharacterView : MonoBehaviour
    {
        [field:SerializeField] public float MoveSpeed { get; private set; }
        [field:SerializeField] public int MaxCarryCapacity { get; private set; }
        [field: SerializeField] public Transform ItemHoldPosition { get; private set; }
        public EcsPackedEntityWithWorld Entity { get; private set; }

        public void Construct(EcsPackedEntityWithWorld entity)
        {
            Entity = entity;
        }
        public void Move(Vector3 desired)
        {
            transform.position += desired;
        }
    }
}