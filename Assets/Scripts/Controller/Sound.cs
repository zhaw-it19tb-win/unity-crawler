using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public bool loop;
    public String name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;
}
