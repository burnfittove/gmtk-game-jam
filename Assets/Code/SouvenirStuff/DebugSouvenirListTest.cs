using System;
using UnityEngine;

public class DebugSouvenirListTest : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.color = Color.softRed;
        
        if (!GameEventManager.instance) return;
        GameEventManager.instance.souvenirEvents.SouvenirListComplete += ChangeColorOnCompletion;
        GameEventManager.instance.souvenirEvents.SouvenirListIncomplete += ChangeColorOnIncompletion;
    }

    private void ChangeColorOnCompletion()
    {
        _spriteRenderer.color = Color.darkOliveGreen;
    }

    private void ChangeColorOnIncompletion()
    {
        _spriteRenderer.color = Color.softRed;
    }
}
