using System.Linq;
using UnityEngine;

public class SouvenirManager : MonoBehaviour
{
    public static SouvenirManager instance;

    public GameObject[] souvenirs { get; private set; }
    public Sprite[] souvenirSprites { get; private set; }
    public Color[] souvenirColors { get; private set; }

    [Header("Souvenir Settings")]
    [SerializeField] private int souvenirCount = 3;

    [Header("UI")]
    [SerializeField] private SouvenirUI souvenirUI;

    private bool _isSouvenirsCollected;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError(
                "There are multiple instances of Souvenir Manager!"
            );

            gameObject.SetActive(false);
            return;
        }

        instance = this;

        souvenirs = new GameObject[souvenirCount];
        souvenirSprites = new Sprite[souvenirCount];
        souvenirColors = new Color[souvenirCount];
    }

    private void Start()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance
                .souvenirEvents
                .AddSouvenir += AddSouvenir;

            GameEventManager.instance
                .souvenirEvents
                .RemoveSouvenir += RemoveSouvenir;
        }

        RefreshUI();
    }

    private void OnDestroy()
    {
        if (GameEventManager.instance != null)
        {
            GameEventManager.instance
                .souvenirEvents
                .AddSouvenir -= AddSouvenir;

            GameEventManager.instance
                .souvenirEvents
                .RemoveSouvenir -= RemoveSouvenir;
        }

        if (instance == this)
        {
            instance = null;
        }
    }

    private void AddSouvenir(GameObject souvenir)
    {
        if (souvenir == null)
        {
            Debug.LogWarning(
                "[SouvenirManager] AddSouvenir dobio NULL!"
            );

            return;
        }

        if (IsSouvenirCollected(souvenir))
        {
            Debug.Log(
                "[SouvenirManager] Souvenir je već skupljen: "
                + souvenir.name
            );

            return;
        }

        SpriteRenderer spriteRenderer =
            FindSouvenirRenderer(souvenir);

        if (spriteRenderer == null)
        {
            Debug.LogError(
                "[SouvenirManager] Nisam pronašao SpriteRenderer na souveniru: "
                + souvenir.name
            );

            return;
        }

        if (spriteRenderer.sprite == null)
        {
            Debug.LogError(
                "[SouvenirManager] SpriteRenderer nema sprite na objektu: "
                + souvenir.name
            );

            return;
        }

        int emptySlot = -1;

        for (int i = 0; i < souvenirs.Length; i++)
        {
            if (souvenirs[i] == null)
            {
                emptySlot = i;
                break;
            }
        }

        if (emptySlot == -1)
        {
            Debug.LogWarning(
                "[SouvenirManager] Souvenir lista je puna!"
            );

            return;
        }

        // Spremi GameObject
        souvenirs[emptySlot] = souvenir;

        // Spremi sprite
        souvenirSprites[emptySlot] =
            spriteRenderer.sprite;

        // Spremi boju
        souvenirColors[emptySlot] =
            spriteRenderer.color;

        Debug.Log(
            "[SouvenirManager] Dodan: "
            + souvenir.name
            + " | Slot: "
            + emptySlot
            + " | Sprite: "
            + spriteRenderer.sprite.name
            + " | Color: "
            + spriteRenderer.color
        );

        RefreshUI();

        // Ako postoji barem jedan prazan slot,
        // lista još nije puna
        if (souvenirs.Any(s => s == null))
        {
            return;
        }

        if (GameEventManager.instance != null)
        {
            GameEventManager.instance
                .souvenirEvents
                .OnSouvenirListComplete();
        }

        _isSouvenirsCollected = true;
    }

    private void RemoveSouvenir()
    {
        /*
         * PROMIJENJENA JE SAMO LOGIKA OVE METODE.
         *
         * Umjesto uklanjanja zadnjeg souvenira,
         * nasumično pokušavamo pronaći jedan
         * postojeći souvenir.
         */

        // Ako nema nijednog souvenira, prekini.
        if (souvenirs.All(s => s == null))
        {
            Debug.Log(
                "[SouvenirManager] Nema souvenira za ukloniti."
            );

            return;
        }

        int randomSlot;

        /*
         * Odaberi nasumični index.
         *
         * Ako slučajno odaberemo prazan slot,
         * ponavljamo sve dok ne pronađemo
         * slot koji sadrži souvenir.
         */
        do
        {
            randomSlot =
                Random.Range(
                    0,
                    souvenirs.Length
                );

        } while (souvenirs[randomSlot] == null);

        Debug.Log(
            "[SouvenirManager] Uklonjen nasumični souvenir: "
            + souvenirs[randomSlot].name
            + " | Slot: "
            + randomSlot
        );

        // Makni GameObject
        souvenirs[randomSlot] = null;

        // Makni njegov sprite iz UI podataka
        souvenirSprites[randomSlot] = null;

        // Resetiraj njegovu boju
        souvenirColors[randomSlot] =
            Color.white;

        // Osvježi UI
        RefreshUI();

        // Ako lista prije nije bila kompletna,
        // ne treba slati incomplete event.
        if (!_isSouvenirsCollected)
        {
            return;
        }

        if (GameEventManager.instance != null)
        {
            GameEventManager.instance
                .souvenirEvents
                .OnSouvenirListIncomplete();
        }

        _isSouvenirsCollected = false;
    }

    private SpriteRenderer FindSouvenirRenderer(
        GameObject souvenir
    )
    {
        SpriteRenderer[] renderers =
            souvenir.GetComponentsInChildren<SpriteRenderer>(
                true
            );

        if (renderers.Length == 0)
        {
            return null;
        }

        // Prvo traži sprite koji nije Circle
        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer == null)
                continue;

            if (renderer.sprite == null)
                continue;

            if (
                renderer.sprite.name
                    .ToLower()
                    .Contains("circle")
            )
            {
                continue;
            }

            return renderer;
        }

        // Ako postoji samo Circle,
        // uzmi prvi validni SpriteRenderer
        foreach (SpriteRenderer renderer in renderers)
        {
            if (
                renderer != null &&
                renderer.sprite != null
            )
            {
                return renderer;
            }
        }

        return null;
    }

    private void RefreshUI()
    {
        if (souvenirUI == null)
        {
            Debug.LogError(
                "[SouvenirManager] SouvenirUI nije dodijeljen u Inspectoru!"
            );

            return;
        }

        souvenirUI.RefreshUI(
            souvenirSprites,
            souvenirColors
        );
    }

    public int GetSouvenirCount()
    {
        return souvenirCount;
    }

    private bool IsArrayEmpty()
    {
        return souvenirs.All(
            souvenir =>
                souvenir == null
        );
    }

    private bool IsSouvenirCollected(
        GameObject souvenir
    )
    {
        return souvenirs.Any(
            collected =>
                collected == souvenir
        );
    }
}