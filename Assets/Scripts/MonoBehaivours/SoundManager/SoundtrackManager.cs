using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private float currentEndTime;
    private Soundtrack queuedTrack;
    private bool shouldQueuedLoop;

    private const float FADE_OUT_DURATION = .5f;

    private void Update()
    {
        if (queuedTrack == null || currentEndTime < Time.time)
            return;

        if (shouldQueuedLoop)
            Play(queuedTrack);
        else
            PlayAndLoop(queuedTrack);

        queuedTrack = null;
    }

    public void Play(Soundtrack track)
    {
        source.loop = false;
        StartCoroutine(PlayTrack(track));
    }

    public void PlayAndLoop(Soundtrack soundtrack)
    {
        source.loop = true;
        StartCoroutine(PlayTrack(soundtrack));
        StartCoroutine(AssignLoopingEndTimeCo());
    }

    public void Enqueue(Soundtrack soundtrack)
    {
        queuedTrack = soundtrack;
        shouldQueuedLoop = false;
    }

    public void EnqueueAndLoop(Soundtrack soundtrack)
    {
        queuedTrack = soundtrack;
        shouldQueuedLoop = true;
    }

    public void Stop(float fadeOutTime)
    {
        if (source.isPlaying)
            StartCoroutine(FadeCurrentTrackOutCo(fadeOutTime));
    }

    private IEnumerator PlayTrack(Soundtrack track)
    {
        if (source.isPlaying)
            yield return StartCoroutine(FadeCurrentTrackOutCo());

        currentEndTime = Time.time + track.Clip.length;
        source.clip = track.Clip;

        if (track.FadesIn)
        {
            source.volume = 0;
            StartCoroutine(FadeTrackInCo(track.FadeInDuration, track.DefaultVolume));
        }
        else
        {
            source.volume = track.DefaultVolume;
        }

        source.Play();
    }

    private IEnumerator FadeCurrentTrackOutCo(float duration = FADE_OUT_DURATION)
    {
        float startVolume = source.volume;
        float startTime = Time.time;
        float endTime = startTime + duration;
        float t;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            t *= startVolume;
            t = -t + startVolume;

            source.volume = t;
            yield return null;
        }

        source.Stop();
    }

    private IEnumerator FadeTrackInCo(float duration, float endVolume)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        float t, volume;

        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            volume = Mathf.Lerp(0, endVolume, t);

            source.volume = volume;
            yield return null;
        }

        source.volume = endVolume;
    }

    private IEnumerator AssignLoopingEndTimeCo()
    {
        float clipLenght = source.clip.length;

        while (source.loop)
        {
            currentEndTime = Time.time + clipLenght;
            yield return new WaitForSeconds(clipLenght);
        }
    }
}
