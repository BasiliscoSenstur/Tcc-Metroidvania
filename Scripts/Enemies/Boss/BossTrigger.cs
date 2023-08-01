using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTrigger : MonoBehaviour
{
    public static BossTrigger instance;

    public GameObject boss;
    public Animator anim;

    public string bossName;

    public Transform camTarget;
    string currentHpBossAnimation;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey(bossName))
            {
                if (PlayerPrefs.GetInt(bossName) != 1)
                {
                    StartActiveBossCo();
                }
            }
            else
            {
                StartActiveBossCo();
            }
        }
    }

    void StartActiveBossCo()
    {
        StartCoroutine(ActiveBossCo());
    }
    public IEnumerator ActiveBossCo()
    {
        boss.gameObject.SetActive(true);

        AudioController.instance.PlayBossMusic();

        yield return new WaitForSeconds(0.42f);

        CameraController.instance.BossCam(1);

        yield return new WaitForSeconds(0.42f);

        BossController.instance.bossHpBar.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.42f);

        ChangeAnimationHpBar("BossCanvasFadeIn");
    }

    public void ChangeAnimationHpBar(string hpBossAnimation)
    {
        if(currentHpBossAnimation == hpBossAnimation)
        {
            return;
        }
        anim.Play(hpBossAnimation);
        currentHpBossAnimation = hpBossAnimation;
    }
}
