using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject sfxSourcePrefab;
    private Transform tf;
    private Queue<AudioSource> sourcesQueue;
    private const int SOURCES_AMOUNT = 5;

    public void Init()
    {
        tf = transform;
        sourcesQueue = new Queue<AudioSource>();

        for (int i = 0; i < SOURCES_AMOUNT; i++)
        {
            var source = Instantiate(sfxSourcePrefab, tf).GetComponent<AudioSource>();
            sourcesQueue.Enqueue(source);
        }
    }

    public void Play(SoundEffect soundEffect)
    {
        var nextSource = sourcesQueue.Dequeue();

        nextSource.pitch = 1;
        nextSource.PlayOneShot(soundEffect.Clip, soundEffect.DefaultVolume);

        sourcesQueue.Enqueue(nextSource);
    }

    public void Play(SoundEffect soundEffect, float volume)
    {
        var nextSource = sourcesQueue.Dequeue();

        nextSource.pitch = 1;
        nextSource.PlayOneShot(soundEffect.Clip, volume);
        
        sourcesQueue.Enqueue(nextSource);
    }

    public void PlayRandPitch(SoundEffect soundEffect, float volume, float minPitch, float maxPitch)
    {
        var nextSource = sourcesQueue.Dequeue();

        nextSource.pitch = Random.Range(minPitch, maxPitch);
        nextSource.PlayOneShot(soundEffect.Clip, volume);

        sourcesQueue.Enqueue(nextSource);
    }
}
