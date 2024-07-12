using System;
using Runtime.Tools;
using UnityEngine;

namespace Runtime.Tools
{
    public class ChildCollider : MonoBehaviour
    {
        public event Action<Collider> OnChildTriggerEnter = delegate { };
        public event Action<Collider> OnChildTriggerExit = delegate { };
        
        public event Action<Collision> OnChildCollisionEnter = delegate { };
        public event Action<Collision> OnChildCollisionExit = delegate { };
      
        private void OnTriggerEnter(Collider other)
        {
            OnChildTriggerEnter?.Invoke(other);
        }
        
        private void OnTriggerExit(Collider other)
        {
            OnChildTriggerExit?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnChildCollisionEnter?.Invoke(collision);
        }
        
        private void OnCollisionExit(Collision collision)
        {
            OnChildCollisionExit?.Invoke(collision);
        }
    }
}