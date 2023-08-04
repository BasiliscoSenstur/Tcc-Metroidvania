using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    CinemachineVirtualCamera cam;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        cam.Follow = PlayerController.instance.transform;
        cam.LookAt = PlayerController.instance.transform;

        if (AudioController.instance.level1.isPlaying)
        {
            AudioController.instance.LevelMusic(2);
        }
        else
        {
            AudioController.instance.LevelMusic(1);
        }
    }

    public void BossCam(int switchObjectCam)
    {
        Transform objectToFollow;

        if (switchObjectCam == 1)
        {
            objectToFollow = BossTrigger.instance.camTarget.transform;
        }
        else
        {
            objectToFollow = PlayerController.instance.transform;
        }

        cam.Follow = objectToFollow;
        cam.LookAt = objectToFollow;
    }
}
