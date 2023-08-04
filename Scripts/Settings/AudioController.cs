using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioSource menu, level1, level2, boss;
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

    public void MenuMusic()
    {
        level1.Stop();
        level2.Stop();
        boss.Stop();
        menu.Play();
    }

    public void LevelMusic(int lvl)
    {
        menu.Stop();
        boss.Stop();
        if(lvl == 1)
        {
            level2.Stop();
            level1.Play();
        }
        if (lvl == 2)
        {
            level1.Stop();
            level2.Play();
        }
    }

    public void BossMusic()
    {
        level1.Stop();
        level2.Stop();
        menu.Stop();
        boss.Play();
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
