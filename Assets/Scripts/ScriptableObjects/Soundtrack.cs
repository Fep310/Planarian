using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soundtrack", menuName = "Sound/New Soundtrack")]
public class Soundtrack : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    public AudioClip Clip => clip;

    [SerializeField][Range(0, 1)] private float defaultVolume;
    public float DefaultVolume => defaultVolume;

    [SerializeField] private bool fadesIn;
    public bool FadesIn => fadesIn;

    [SerializeField] private float fadeInDuration;
    public float FadeInDuration => fadeInDuration;
}
