using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public Collider2D _trigger;

    private void Start()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.souvenirEvents.SouvenirListComplete += AllowEntry;
        GameEventManager.instance.souvenirEvents.SouvenirListIncomplete += DisallowEntry;
    }

    private void AllowEntry()
    {
        _trigger.enabled = true;
    }

    private void DisallowEntry()
    {
        _trigger.enabled = false;
    }
}
