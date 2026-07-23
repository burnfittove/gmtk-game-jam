using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class SouvenirManager : MonoBehaviour
{
    public static SouvenirManager instance;
    public GameObject[] souvenirs;
    public int souvenirCount;
    private bool _isSouvenirsCollected;

    private void Awake()
    {
        if (instance != this && instance)
        {
            Debug.LogError("There are multiple instances of Souvenir Manager!");
            gameObject.SetActive(false);
            return;
        }
        instance = this;    
        
        souvenirs = new GameObject[souvenirCount];
    }

    private void Start()
    {
        if (!GameEventManager.instance) return;
        GameEventManager.instance.souvenirEvents.AddSouvenir += AddSouvenir;
        GameEventManager.instance.souvenirEvents.RemoveSouvenir += RemoveSouvenir;
    } 

    private void AddSouvenir(GameObject souvenir)
    {
        // Check if the souvenir is already in the list
        if (IsSouvenirCollected(souvenir)) return;
            
        for (var i = 0; i < souvenirCount; i++)
        {
            if (souvenirs[i]) continue; // If the slot is not null, continue
            souvenirs[i] = souvenir;                            // Otherwise, add the souvenir to the empty slot
            souvenir.TryGetComponent(out SpriteRenderer sr);    // and get the SpriteRenderer
            if (!sr) continue;
            if (!GameEventManager.instance) return;
            GameEventManager.instance.uiEvents.OnAddSouvenirToList(sr); // Pass the SpriteRenderer to DisplaySouvenirs
            break;
        }

        if (souvenirs.Any(s => !s)) // If the list is not yet full, return
        {
            return;
        }
        if (!GameEventManager.instance) return;
        GameEventManager.instance.souvenirEvents.OnSouvenirListComplete();  // Otherwise, signal to all subscribers that the list is complete
        _isSouvenirsCollected = true;                                       // and mark it as such
    }

    private void RemoveSouvenir()
    {
        if (IsArrayEmpty()) return;
        
        int randomSlot;
        do
        {
            randomSlot = Random.Range(0, souvenirs.Length);
        } while (!souvenirs[randomSlot]);
        
        souvenirs[randomSlot].TryGetComponent(out SpriteRenderer sr);    // Check for SpriteRenderer component
        if (!sr) return;    // Failsafe
        if (!GameEventManager.instance) return;
        GameEventManager.instance.uiEvents.OnRemoveSouvenirFromList(sr);    // Pass the SpriteRenderer to DisplaySouvenirs for sprite comparison
        souvenirs.SetValue(null, randomSlot);   // Remove souvenir from list

        if (!_isSouvenirsCollected) return;
        if (!GameEventManager.instance) return;
        GameEventManager.instance.souvenirEvents.OnSouvenirListIncomplete();    // Signal to all subscribers that the list is no longer complete if it was complete prior 
    }
    
    public int GetSouvenirCount()
    {
        return souvenirCount;
    }

    private bool IsArrayEmpty()
    {
        return souvenirs.All(s => !s);
    }

    private bool IsSouvenirCollected(GameObject souvenir)
    {
        return souvenirs.Any(s => s == souvenir);
    }
}
