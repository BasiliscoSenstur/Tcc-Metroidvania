using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public static GateController instance;
    PlayerController player;
    Animator anim;

    public Transform exitPoint;
    public string levelToLoad;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = PlayerController.instance;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void OpenGate(bool status)
    {
        anim.SetBool("OpenGate", status);
        if (status == true)
        {
            AudioController.instance.PlaySfx(2);
        }
        else
        {
            AudioController.instance.PlaySfx(1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(UseGateCo());

            player.transform.position = Vector3.MoveTowards
                (player.transform.position, exitPoint.position, player.moveSpeed * Time.fixedDeltaTime);
        }
    }

    IEnumerator UseGateCo()
    {
        player.noInput = true;
        UIController.instance.FadeScreen("FadeToBlack");

        yield return new WaitForSeconds(1.5f);

        //Save
        PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        PlayerPrefs.SetFloat("PosX", exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY", exitPoint.position.y);

        PlayerSpawnController.instance.SetSpawnPoint(exitPoint.position);

        SceneManager.LoadScene(levelToLoad);

        if (AudioController.instance.level1.isPlaying)
        {
            AudioController.instance.LevelMusic(2);
        }
        else
        {
            AudioController.instance.LevelMusic(1);
        }

        UIController.instance.FadeScreen("FadeFromBlack");
        player.noInput = false;
    }
}
