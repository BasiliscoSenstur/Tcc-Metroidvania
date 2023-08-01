using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSource mainMenuMusic, levelMusic, bossMusic;
    public AudioSource[] soundEfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            levelMusic.Play();
            bossMusic.Stop();
            mainMenuMusic.Stop();
        }
    }
    public void PlayBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void PlaySfx(int efxToPlay)
    {
        soundEfx[efxToPlay].Stop();
        soundEfx[efxToPlay].Play();
    }

    public void PlaySfxAdjusted(int efxToPlayAdjusted)
    {
        soundEfx[efxToPlayAdjusted].pitch = Random.Range(.8f, 1.2f);
        PlaySfx(efxToPlayAdjusted);
    }
}
