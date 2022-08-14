using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EventTransitionData
{
    public Sprite Sprite;
    public float FadeInTime;
    public float ImageWaitTime;
    public List<string> Texts;
    public SoundEffect Sound;
    public Soundtrack Track;
    public bool ShouldTrackLoop;
    public bool ShouldEnqueueTrack;
}
