using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioController manager;

    public void Awake()
    {
        if(AudioController.instance == null) 
        {
            AudioController newManager = Instantiate(manager);
            AudioController.instance = newManager;
            DontDestroyOnLoad(manager.gameObject);
        }
    }
}
