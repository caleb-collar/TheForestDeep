using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    // "Bobbing" animation from 1D Perlin noise.

    // Range over which height varies.
    [SerializeField] private float heightScale = 1.0f;

    // Distance covered per second along X axis of Perlin plane.
    [SerializeField] private float xScale = 1.0f;


    void Update()
    {
        float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);
        Vector3 pos = transform.position;
        pos.y = height;
        transform.position = pos;
    }
}
