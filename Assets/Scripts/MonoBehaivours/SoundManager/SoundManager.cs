using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : InitializableMonoBehaviour
{
    [SerializeField] private SoundEffectManager sfxManager;
    [SerializeField] private SoundtrackManager soundtrackManager;

    public override void Init()
    {
        sfxManager.Init();
        soundtrackManager.Init();
    }

    public void PlaySoundEffect(SoundEffect soundEffect)
    {
        sfxManager.Play(soundEffect);
    }

    public void PlaySoundEffect(SoundEffect soundEffect, float volume)
    {
        sfxManager.Play(soundEffect, volume);
    }

    public void PlaySoundEffectRandPitch(SoundEffect soundEffect, float volume, float maxPitch, float minPitch)
    {
        sfxManager.PlayRandPitch(soundEffect, volume, maxPitch, minPitch);
    }

    public void PlaySoundtrack() { }
}
