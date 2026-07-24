using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    public float zoomInLens;
    private float initialLens;
    private CinemachineCamera _camera;
    private bool _zoomIn;

    private void Awake()
    {
        _camera = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        initialLens = _camera.Lens.OrthographicSize;

        if (!GameEventManager.instance) return;
        GameEventManager.instance.miscellaneousEvents.SlowDown += ZoomIn;
        GameEventManager.instance.miscellaneousEvents.SpeedUp += ZoomOut;
    }

    private void Update()
    {
        if (_zoomIn)
        {
            _camera.Lens.OrthographicSize = Mathf.Lerp(_camera.Lens.OrthographicSize, zoomInLens, Time.deltaTime * 5);
            return;
        }
        
        _camera.Lens.OrthographicSize = Mathf.Lerp(_camera.Lens.OrthographicSize, initialLens, Time.deltaTime * 5);
    }

    private void ZoomIn()
    {
        _zoomIn = true;
    }

    private void ZoomOut()
    {
        _zoomIn = false;
    }
}
