using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public SoundAudioClip[] soundAudioClipArray;
    private Dictionary<Sound, float> soundTimerDictionary;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Walk] = 0f;
    }
    public void PlayerSound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
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
                    float lasttimePlayed = soundTimerDictionary[sound];
                    float playerWalkTimerMax = 0.25f;
                    if (lasttimePlayed + playerWalkTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                break;
        }

        return true;
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }
}

[System.Serializable]
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
    Losing,
}