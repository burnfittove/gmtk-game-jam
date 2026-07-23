using UnityEngine;
using UnityEngine.UI;

public class DisplaySouvenirs : MonoBehaviour
{
    public Image[] slots;

    private void Start()
    {
        HideAllSlots();

        if (!GameEventManager.instance) return;
        GameEventManager.instance.uiEvents.AddSouvenirToList += AddSouvenir;
        GameEventManager.instance.uiEvents.RemoveSouvenirFromList += RemoveSouvenir;
    }

    // private void UpdateUI()
    // {
    //     for (var i = 0; i < SouvenirManager.instance.souvenirCount; i++)
    //     {
    //         if (!SouvenirManager.instance.souvenirs[i])
    //         {
    //             HideSlot(slots[i]);
    //             continue;
    //         }
    //
    //         SouvenirManager.instance.souvenirs[i].TryGetComponent(out SpriteRenderer sr);
    //         if (!sr) continue;
    //         ShowSlot(slots[i], sr);
    //     }
    // }

    private void AddSouvenir(SpriteRenderer sr)
    {
        foreach (var slot in slots)
        {
            if (slot.isActiveAndEnabled) continue;  // Find an inactive slot
            ShowSlot(slot, sr);                     // and fill it
            return;
        }
    }

    private void RemoveSouvenir(SpriteRenderer sr)
    {
        foreach (var slot in slots)
        {
            if (!slot.isActiveAndEnabled) continue; // Find an active slot,
            if (slot.sprite != sr.sprite) continue; // compare its sprite to that of the provided component
            HideSlot(slot);                         // and hide it
            return;
        }
    }


    private void HideSlot(Image slot)
    {
        slot.gameObject.SetActive(false);
    }

    private void ShowSlot(Image slot, SpriteRenderer sr)
    {
        slot.sprite = sr.sprite;
        slot.color = sr.color;
        slot.gameObject.SetActive(true);
    }

    private void HideAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}
