using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public int numToPool;
    public GameObject bullet;
    private List<GameObject> _bullets;

    private void Awake()
    {
        _bullets = new List<GameObject>();
    }

    private void Start()
    {
        for (var i = 0; i < numToPool; i++)
        {
            _bullets.Add(CreateBullet());
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in _bullets)
        {
            if (bullet.activeInHierarchy) continue;
            return bullet;
        }
        
        var bul = CreateBullet();
        _bullets.Add(bul);
        return bul;
    }

    private GameObject CreateBullet()
    {
        var bul = Instantiate(bullet);
        bul.SetActive(false);
        return bul;
    }
}
