using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnController : MonoBehaviour
{
    public static PlayerSpawnController instance;
    GameObject player;
    Vector2 spawnPosition;

    string fadeToBlack, fadeFromBlack;
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
        player = PlayerController.instance.gameObject;
        spawnPosition = player.transform.position;
    }

    void Update()
    {

    }

    public void SetSpawnPoint(Vector2 newSpawnPoint)
    {
        spawnPosition = newSpawnPoint;
    }

    public void StartRespawnPlayerCo()
    {
        StartCoroutine(RespawnPlayerCo());
    }

    public IEnumerator RespawnPlayerCo()
    {
        UIController.instance.FadeScreen("FadeToBlack");

        PlayerController.instance.gameObject.transform.position = spawnPosition;

        player.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        PlayerHealthController.instance.FillHP();

        player.SetActive(true);

        UIController.instance.FadeScreen("FadeFromBlack");
    }
}
