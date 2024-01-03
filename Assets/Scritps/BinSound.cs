using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSound : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;
    public AudioSource AudioSource;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayOneShot(openSound);
    }

    private void OnTriggerExit(Collider other)
    {
        AudioSource.PlayOneShot(closeSound);
    }

    private void Start()
    {
        AudioSource = GetComponentInChildren<AudioSource>();
    }
}
