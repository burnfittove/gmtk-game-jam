using Code.Events;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;
    public InputEvents inputEvents;
    public MiscellaneousEvents miscellaneousEvents;

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
        miscellaneousEvents = new MiscellaneousEvents();
    }

    private void Start()
    {
        miscellaneousEvents.QuitGame += QuitGame;
    }


    private void QuitGame()
    {
        Application.Quit();
    }
}
