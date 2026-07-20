using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;
    public InputEvents inputEvents;

    private void Awake()
    {
        if (instance != this && instance)
        {
            Debug.LogWarning("There are multiple instances of GameEventManager!");
            gameObject.SetActive(false);
            return;
        }
        instance = this;
        
        inputEvents = new InputEvents();
    }
}
