using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource.Play();                 
    }
}
