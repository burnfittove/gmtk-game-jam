using UnityEngine;

public class PlaySoundOnClick : MonoBehaviour
{
    public AudioClip buttonPress;

    public void OnClick()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.audioEvents.OnPlay(buttonPress);
    }
}
