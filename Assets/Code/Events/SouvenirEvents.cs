using System;
using UnityEngine;

public class SouvenirEvents
{
    public event Action SouvenirListComplete;

    public void OnSouvenirListComplete()
    {
        SouvenirListComplete?.Invoke();
    }

    public event Action SouvenirListIncomplete;

    public void OnSouvenirListIncomplete()
    {
        SouvenirListIncomplete?.Invoke();
    }

    public event Action<GameObject> AddSouvenir;

    public void OnAddSouvenir(GameObject souvenir)
    {
        AddSouvenir?.Invoke(souvenir);
    }

    public event Action RemoveSouvenir;
    
    public void OnRemoveSouvenir()
    {
        RemoveSouvenir?.Invoke();
    }

}
