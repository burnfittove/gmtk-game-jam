using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animation animationComponent;
    [SerializeField] private AnimationClip[] transitionClips;

    private void Awake()
    {
        if (animationComponent == null)
            animationComponent = GetComponent<Animation>();

        foreach (AnimationClip clip in transitionClips)
        {
            if (clip == null)
                continue;

            animationComponent.AddClip(clip, clip.name);
        }
    }

    public void PlayTransition(string animationName)
    {
        if (animationComponent == null)
        {
            Debug.LogError("Animation component is missing!");
            return;
        }

        if (animationComponent.GetClip(animationName) == null)
        {
            Debug.LogError("Animation not found: " + animationName);
            return;
        }

        animationComponent.Play(animationName);
    }
}