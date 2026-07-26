using UnityEngine;

public class QuickParalax : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector3.left * (speed * Time.deltaTime));
    }
}
