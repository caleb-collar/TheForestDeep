using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shields : MonoBehaviour
{
    [SerializeField] private int health = 1000;
    [SerializeField] private GameObject shieldFXObj;
    private float fade = 0.15f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null && other.gameObject.CompareTag("EnemyP"))
        {
            ProcessHit(damageDealer);
            //Debug.Log("Player hit");
        }
    }
    private void ProcessHit(DamageDealer damageDealer)
    {
        GameObject shieldHitFX = (GameObject) Instantiate(shieldFXObj, transform.position, Quaternion.identity);
        shieldHitFX.transform.parent = gameObject.transform;
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        Destroy(shieldHitFX, fade);
        if (health <= 0) Destroy(gameObject);
    }
}
