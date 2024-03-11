using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public SoundAudioClip[] soundAudioClipArray;
    private Dictionary<Sound, float> soundTimerDictionary;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Walk] = 0f;
    }

    public void PlayerSound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            var soundGameObject = new GameObject("Sound");
            var audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
            Destroy(soundGameObject, 1);
        }
    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.Walk:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    var lasttimePlayed = soundTimerDictionary[sound];
                    var playerWalkTimerMax = 0.25f;
                    if (lasttimePlayed + playerWalkTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }

                    return false;
                }

                break;
        }

        return true;
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (var soundAudioClip in soundAudioClipArray)
            if (soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;
        return null;
    }
}

[Serializable]
public class SoundAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
}

public enum Sound
{
    Walk,
    Attack,
    PlayerHit,
    PlayerDie,
    EnemyHit,
    EnemyDie,
    Pick,
    ButtonClicked,
    Transition,
    Winning,
    Losing
}