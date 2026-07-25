using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChooseRandomImage : MonoBehaviour
{
    public Image imageObject;
    public TMP_Text textObject;
    public Sprite[] images;
    public string[] flavourText;

    private void Awake()
    {
        if (images.Length != flavourText.Length)
        {
            Debug.LogWarning("kys bro. the arrays aren't the same... don't pmo.");
            return;
        }
        
        var randomIndex = Random.Range(0, images.Length);
        
        imageObject.sprite = images[randomIndex];
        textObject.text = flavourText[randomIndex];
    }
}
