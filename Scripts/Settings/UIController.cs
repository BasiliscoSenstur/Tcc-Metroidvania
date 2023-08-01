using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] Slider HealthBar;

    public Image fadeScreen;
    [SerializeField] Animator anim;

    public GameObject pauseScreen;

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
        fadeScreen.gameObject.SetActive(true);
    }
    void Start()
    {
        HealthBar.maxValue = PlayerHealthController.instance.maxHP;
        UpdateHealthDisplay();
        FadeScreen("FadeFromBlack");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public void UpdateHealthDisplay()
    {
        HealthBar.value = PlayerHealthController.instance.currentHP;
    }

    public void FadeScreen(string newFade)
    {
        anim.Play(newFade);
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            PlayerController.instance.noInput = true;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            PlayerController.instance.noInput = false;
        }
    }

    public void MainMenu()
    {
        StartCoroutine(MainMenuCo());
    }

    public IEnumerator MainMenuCo()
    {
        Time.timeScale = 1;

        FadeScreen("FadeToBlack");

        yield return new WaitForSeconds(1f);

        Destroy(PlayerController.instance.gameObject);
        PlayerController.instance = null;

        Destroy(PlayerSpawnController.instance.gameObject);
        PlayerSpawnController.instance = null;

        SceneManager.LoadScene("Main Menu");

        yield return new WaitForSeconds(0.1f);

        FadeScreen("FadeFromBlack");

        instance = null;
        Destroy(gameObject);
    }
}
