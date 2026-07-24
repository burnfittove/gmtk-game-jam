using UnityEngine;

public class sfxFunctions : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSFX;

    public void PlayClickSFX()
    {
        if (audioSource != null && clickSFX != null)
        {
            audioSource.PlayOneShot(clickSFX);
        }
    }
}