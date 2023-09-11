using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    [Range(0, 1)] public float volume = .3f;
    [Range(0, 1)] public float pitch = 1f;
    [HideInInspector] public AudioSource audioSource;
    public bool loop;
}
