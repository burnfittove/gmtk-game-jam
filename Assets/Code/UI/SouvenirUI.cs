using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SouvenirUI : MonoBehaviour
{
    [Header("UI Image slotovi")]
    [SerializeField] private List<Image> souvenirImages = new List<Image>();

    private void Awake()
    {
        if (souvenirImages == null || souvenirImages.Count == 0)
        {
            Debug.LogWarning(
                "[SouvenirUI] Souvenir Images lista je prazna!"
            );

            return;
        }

        for (int i = 0; i < souvenirImages.Count; i++)
        {
            if (souvenirImages[i] == null)
                continue;

            souvenirImages[i].sprite = null;
            souvenirImages[i].gameObject.SetActive(false);
        }
    }

    public void RefreshUI(
        Sprite[] sprites,
        Color[] colors
    )
    {
        if (sprites == null || colors == null)
        {
            Debug.LogError(
                "[SouvenirUI] Sprites ili Colors array je NULL!"
            );

            return;
        }

        for (int i = 0; i < souvenirImages.Count; i++)
        {
            Image currentImage = souvenirImages[i];

            if (currentImage == null)
                continue;

            if (i >= sprites.Length || sprites[i] == null)
            {
                HideSlot(currentImage);
                continue;
            }

            currentImage.gameObject.SetActive(true);
            currentImage.enabled = true;

            // Postavi sprite
            currentImage.sprite = sprites[i];

            // BITNO:
            // Prenesi boju sa prefab SpriteRenderera
            if (i < colors.Length)
            {
                currentImage.color = colors[i];
            }

            currentImage.preserveAspect = true;

            Debug.Log(
                "[SouvenirUI] Slot " +
                i +
                " | Sprite: " +
                sprites[i].name +
                " | Color: " +
                currentImage.color
            );
        }
    }

    private void HideSlot(Image image)
    {
        if (image == null)
            return;

        image.sprite = null;
        image.gameObject.SetActive(false);
    }
}