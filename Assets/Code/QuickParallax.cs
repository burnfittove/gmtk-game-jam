using UnityEngine;

namespace Code
{
    public class QuickParallax : MonoBehaviour
    {
        public float speed;

        private void Update()
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
    }
}
