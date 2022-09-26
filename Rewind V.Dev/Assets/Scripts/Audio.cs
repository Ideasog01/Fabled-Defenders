using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Audio
{
    public string name;

    public AudioClip clip;

    [Range(0f, 3f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(.1f, 3f)]
    public float maxRange;
    [Range(.1f, 3f)]
    public float minRange;

    public bool loop;
    public bool playOnWake;


    [HideInInspector]
    public AudioSource source;
}

