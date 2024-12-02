using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string clipName;

    public enum AudioTypes { soundEffect, music }
    public AudioTypes audioType;

    [HideInInspector] public AudioSource source;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;

    public bool Loop;

    [HideInInspector]
    public float playbackPosition;
}
