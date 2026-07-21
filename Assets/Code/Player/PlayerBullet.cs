using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.right * (2 * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;
        gameObject.SetActive(false);
    }
}
