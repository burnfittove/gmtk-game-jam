using System;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject providedSouvenir;
    [SerializeField] private AudioSource audioSource; 


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        // var s = Instantiate(providedSouvenir, transform.position, transform.rotation);  // Instantiate a copy of the souvenir
        if (!GameEventManager.instance) return; // If there is no GameEventManager, return
        GameEventManager.instance.souvenirEvents.OnAddSouvenir(providedSouvenir);  // Otherwise, add the souvenir

        audioSource.Play(); 


    }
}
