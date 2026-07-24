using DG.Tweening;
using UnityEngine;

public class UIHoverManager : MonoBehaviour
{
    [System.Serializable]
    public class HoverGroup
    {
        public RectTransform[] uiElements;

        [HideInInspector]
        public Vector3[] originalScales;
    }

    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float duration = 0.25f;

    private void Awake()
    {
        foreach (var group in groups)
        {
            group.originalScales = new Vector3[group.uiElements.Length];

            for (int i = 0; i < group.uiElements.Length; i++)
            {
                group.originalScales[i] = group.uiElements[i].localScale;
            }
        }
    }

    [SerializeField] private HoverGroup[] groups;

    public void HoverEnter(int index)
    {
        if (index < 0 || index >= groups.Length) return;

        var group = groups[index];

        for (int i = 0; i < group.uiElements.Length; i++)
        {
            group.uiElements[i].DOKill();
            group.uiElements[i]
                .DOScale(group.originalScales[i] * hoverScale, duration)
                .SetEase(Ease.OutBack);
        }
    }

    public void HoverExit(int index)
    {
        if (index < 0 || index >= groups.Length) return;

        var group = groups[index];

        for (int i = 0; i < group.uiElements.Length; i++)
        {
            group.uiElements[i].DOKill();
            group.uiElements[i]
                .DOScale(group.originalScales[i], duration)
                .SetEase(Ease.OutBack);
        }
    }
}