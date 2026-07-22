using UnityEngine;

public class RemoveSouvenirFromList : MonoBehaviour
{
    public void RemoveSouvenir()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.souvenirEvents.OnRemoveSouvenir();
    }
}
