using UnityEngine;
using UnityEngine.UI;

public class DisplaySouvenirs : MonoBehaviour
{
    public Image[] slots;
    public Sprite emptySlot;

    private void Start()
    {
        Initialize();

        if (!GameEventManager.instance) return;
        GameEventManager.instance.uiEvents.AddSouvenirToList += AddSouvenir;
        GameEventManager.instance.uiEvents.RemoveSouvenirFromList += RemoveSouvenir;
    }

    private void AddSouvenir(SpriteRenderer sr)
    {
        foreach (var slot in slots)
        {
            if (slot.sprite != emptySlot) continue;  // Find an inactive slot
            AddSouvenir(slot, sr);                     // and fill it
            return;
        }
    }

    private void RemoveSouvenir(SpriteRenderer sr)
    {
        foreach (var slot in slots)
        {
            if (slot.sprite == emptySlot) continue; // Find an active slot,
            if (slot.color != sr.color) continue; // compare its sprite to that of the provided component
            RemoveSouvenir(slot);                         // and hide it
            return;
        }
    }


    private void RemoveSouvenir(Image slot)
    {
        slot.sprite = emptySlot;
        slot.color = Color.white;
    }

    private void AddSouvenir(Image slot, SpriteRenderer sr)
    {
        slot.sprite = sr.sprite;
        slot.color = sr.color;
    }

    private void Initialize()
    {
        foreach (var slot in slots)
        {
            slot.sprite = emptySlot;
        }
    }
}
