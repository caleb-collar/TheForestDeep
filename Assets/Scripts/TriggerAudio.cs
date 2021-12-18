using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip triggerClip;
    [SerializeField] private AudioSource source;

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        source.clip = triggerClip;
        source.Play();
        Debug.Log("Triggered...");
    }
}
