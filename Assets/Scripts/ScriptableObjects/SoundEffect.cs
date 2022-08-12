using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SoundEffect", menuName = "Sound/New SoundEffect")]
public class SoundEffect : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    public AudioClip Clip => clip;
    [SerializeField] [Range(0, 1)] private float defaultVolume;
    public float DefaultVolume => defaultVolume;
}
