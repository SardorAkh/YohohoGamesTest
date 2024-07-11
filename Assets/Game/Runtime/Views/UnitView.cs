using UnityEngine;

namespace Runtime.Views
{
    public class UnitView : MonoBehaviour
    {
        public void Move(Vector3 translation)
        {
            transform.Translate(translation);
        }
        
    }
}