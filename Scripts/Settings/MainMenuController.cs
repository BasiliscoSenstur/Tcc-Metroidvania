using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueGame;
    public GameObject options;
    public PlayerController player;

    void Start()
    {
        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueGame.gameObject.SetActive(true);
        }

        AudioController.instance.PlayMainMenuMusic();
    }

    public void ContinueGame()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));

        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Options()
    {
        if (options.gameObject.activeSelf)
        {
            options.gameObject.SetActive(false);
        }
        else
        {
            options.gameObject.SetActive(true);
        }
    }
}
