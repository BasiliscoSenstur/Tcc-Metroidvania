using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string newGameScene;

    public GameObject continueGame;
    public PlayerController player;

    void Start()
    {
        AudioController.instance.MenuMusic();

        if (PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueGame.gameObject.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        player.gameObject.SetActive(true);
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));

        AudioController.instance.PlaySfx(9);
        SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        player.gameObject.SetActive(true);
        SceneManager.LoadScene(newGameScene);
        AudioController.instance.PlaySfx(9);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
        AudioController.instance.PlaySfx(9);

    }
}
