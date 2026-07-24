using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    private GameObject[] npcs;
    public RuntimeAnimatorController[] animators;

    private void Start()
    {
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        
        if (npcs.Length == 0) return;
        
        foreach (var npc in npcs)
        {
            npc.transform.GetChild(0).TryGetComponent(out Animator anim);
            if (!anim) continue;
            var randomIndex = Random.Range(0, animators.Length);
            anim.runtimeAnimatorController = animators[randomIndex];
        }
    }
}
