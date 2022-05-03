using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticleEffectManager : MonoBehaviour
{
    public static UIParticleEffectManager Instance;

    [SerializeField]
    private ParticleSystem CoinPE;
    [SerializeField]
    private ParticleSystem PunyaPE;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayCoinPE()
    {
        CoinPE.Play();
    }

    public void PlayPunyaPE()
    {
        PunyaPE.Play();
    }
}