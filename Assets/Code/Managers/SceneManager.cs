using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public Image imageTransitionObject;
    public float fadeTime;
    private float _fadeTimeBuffer;
    private readonly Color _alphaZero = new(0, 0, 0, 0);
    private readonly Color _alphaOne = new(0, 0, 0, 1);
    public bool _fadeIn;
    public bool _fadeOut;
    private string _sceneName;

    private void Awake()
    {
        imageTransitionObject.gameObject.SetActive(true);
        
        if (_fadeOut)
        {
            imageTransitionObject.color = _alphaOne;
            return;
        }
        
        imageTransitionObject.color = _alphaZero;
    }

    private void Start()
    {
        _fadeTimeBuffer = fadeTime;
        
        if (!GameEventManager.instance) return;
        GameEventManager.instance.sceneEvents.LoadScene += LoadScene;
    }

    private void Update()
    {
        
        if (_fadeIn)
        {
            imageTransitionObject.color = Color.Lerp(imageTransitionObject.color, _alphaOne, fadeTime * Time.deltaTime);
            _fadeTimeBuffer -= Time.deltaTime;
            if (_fadeTimeBuffer > 0) return;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneName);
        }

        if (!_fadeOut) return;
        imageTransitionObject.color = Color.Lerp(imageTransitionObject.color, _alphaZero, fadeTime * Time.deltaTime);
        
    }

    private void LoadScene(string sceneName)
    {
        _fadeIn = true;
        _sceneName = sceneName;
    }
    
    // private void FadeIn()
    // {
    //     _fadeIn = true;
    //     _fadeOut = false;
    // }
    //
    // private void FadeOut()
    // {
    //     _fadeIn = false;
    //     _fadeOut = true;
    // }
}
