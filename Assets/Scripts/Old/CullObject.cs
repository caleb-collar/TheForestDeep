using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Cull Object & Shredder | Caleb A. Collar | 10.7.21

public class CullObject : MonoBehaviour
{
    private float xMin, xMax, yMin, yMax;
    private void Start()
    {
        SetupWorldBoundaries();
    }

    void Update()
    {
        CullProjectile();
    }

    void CullProjectile()
    {
        if (gameObject.transform.position.y > yMax || gameObject.transform.position.y < yMin || gameObject.transform.position.x > xMax || gameObject.transform.position.x < xMin)
        {
            Destroy(gameObject);
        }
    }
    
    void SetupWorldBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }
}
