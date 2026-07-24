using UnityEngine;

public class CutsceneChangeScene : MonoBehaviour
{
    public float timer;
    private float _timer;
    public string nextScene;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer > 0) return;
        if (!GameEventManager.instance) return;
        GameEventManager.instance.sceneEvents.OnLoadScene(nextScene);
    }
}
