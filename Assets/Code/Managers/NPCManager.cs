using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    private GameObject[] npcs;
    public Sprite[] sprites;

    private void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        
        if (npcs.Length == 0) return;
        
        foreach (var npc in npcs)
        {
            npc.transform.GetChild(0).TryGetComponent(out SpriteRenderer spriteRenderer);
            if (!spriteRenderer) continue;
            var randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];
        }
    }
}
