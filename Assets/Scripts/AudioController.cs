using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    [Header("Sounds")]
    public AudioSource HeroShoot;
    public AudioSource HeroStab;
    public AudioSource HeroMagic;
    public AudioSource HeroDeath;
    public AudioSource EnemyStab;
    public AudioSource EnemyDeath;
    public AudioSource MinotarRoar;
    public AudioSource WaveStart;

    public static AudioController Instance;

    public AudioController()
    {
        Instance = this;
    }

    public void PlayHeroShoot()
    {
        HeroShoot.Play();
    }

    public void PlayHeroStab()
    {
        HeroStab.Play();
    }

    public void PlayHeroMagic()
    {
        HeroMagic.Play();
    }

    public void PlayHeroDeath()
    {
        HeroDeath.Play();
    }

    public void PlayEnemyStab()
    {
        EnemyStab.Play();
    }

    public void PlayEnemyDeath()
    {
        EnemyDeath.Play();
    }

    public void PlayMinotarRoar()
    {
        MinotarRoar.Play();
    }

    public void PlayWaveStart()
    {
        WaveStart.Play();
    }
}
