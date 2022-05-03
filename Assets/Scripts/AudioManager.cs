using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixerGroup mixerGroup;
    public Sound[] sounds;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.name = s.clip.name;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Muted", 0);
    }

    public void ToggleSound()
    {
        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            AudioManager.Instance.Play("SoundButton");
        }
    }

    public void Play(string sound)
    {
        // Debug.Log("sound: " + sound);
        if(PlayerPrefs.GetInt("Muted") == 0)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            // Debug.Log("Playing : " + s);
            if(s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            // s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            // s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            // Debug.Log("Play");
            s.source.Play();
        }
    }

    public AudioSource GetAudioSourceOf(string audio)
    {
        Sound s = Array.Find(sounds, item => item.name == audio);
        return s.source;
    }
}
