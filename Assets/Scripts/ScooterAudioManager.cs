using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterAudioManager : MonoBehaviour
{
    // TODO Pulkit: Add some serialize fields for controlling the mix/max volume and pitches

    private AudioSource vehicleAudioSource;
    private const string SCOOTER_SOUND = "FinalScooter";
    [Range(0, 1)]
    [SerializeField]
    private float minVolume = 0.15f;
    [Range(0, 1)]
    [SerializeField]
    private float maxVolume = 1;
    [Range(-2, 2)]
    [SerializeField]
    private float minPitch = 0.70f;
    [Range(-1, 2)]
    [SerializeField]
    private float maxPitch = 1.15f;
    [Range(1, 15)]
    [SerializeField]
    private float soundAccelerationSpeed = 10;

    private void Start()
    {
        vehicleAudioSource = AudioManager.Instance.GetAudioSourceOf(SCOOTER_SOUND);

        Invoke("StartBGSound", 0.5f);
    }

    private void StartBGSound()
    {
        AudioManager.Instance.Play(SCOOTER_SOUND);
    }

    public void UpdateSound(float currentSpeed)
    {
        float volume;
        float pitch;

        // print("Speed :" + currentSpeed);
        if (currentSpeed < 0.05f)
        {
            volume = vehicleAudioSource.volume - Time.deltaTime * 2;
            pitch = vehicleAudioSource.pitch - Time.deltaTime;
            volume = Mathf.Clamp(volume, minVolume, maxVolume);
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        }
        else
        {
            volume = vehicleAudioSource.volume + Time.deltaTime * 0.1f * soundAccelerationSpeed;
            pitch = vehicleAudioSource.pitch + Time.deltaTime * 0.1f * soundAccelerationSpeed;

            volume = Mathf.Clamp(volume, minVolume, Mathf.Clamp(currentSpeed, minVolume + 0.05f, 1));
            pitch = Mathf.Clamp(pitch, minPitch, Mathf.Clamp(currentSpeed, 1, maxPitch));
        }
        vehicleAudioSource.volume = volume;
        vehicleAudioSource.pitch = pitch;
    }
    public void StopBackgroundSound()
    {
        vehicleAudioSource.Pause();
    }
    public void ResumeBackgroundSound()
    {
        vehicleAudioSource.volume = 0.1f;
        vehicleAudioSource.Play();
    }
}