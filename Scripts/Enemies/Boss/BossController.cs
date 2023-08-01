using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public static BossController instance;

    public Transform[] spawnPoints;
    public Transform targetPoint;

    //Stats
    public int currentBossHp;

    //Configs
    public EnemySO data;
    public string bossName;
    public Animator anim;
    public Slider bossHpBar;
    public GameObject winObjects;

    //Shot
    public GameObject fireball;
    public Transform firepoint;

    //States
    Boss_Abstract currentState;
    public string STATE;
    
    public State1 state1 = new State1();
    public State2 state2 = new State2();
    public State3 state3 = new State3();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //Health
        bossHpBar.maxValue = data.maxHP;
        currentBossHp = data.maxHP;
        bossHpBar.value = currentBossHp;

        currentState = state1;
        currentState.EnterState(this);
    }

    void Update()
    {
        STATE = currentState.ToString();
        currentState.UpdateLogics(this);
    }

    void FixedUpdate()
    {
        currentState.UpdatePhysics(this);

        //Flip Scale
        if (transform.position.x < PlayerController.instance.transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    public void SwitchState(Boss_Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public void DealDamageBoss(int damage)
    {
        currentBossHp -= damage;
        if (currentBossHp <= 0)
        {
            currentBossHp = 0;
            BossDeath();
        }
        bossHpBar.value = currentBossHp;
    }

    public void BossDeath()
    {
        StartCoroutine(BossDeathCo());
        PlayerPrefs.SetInt(bossName, 1);
    }
    IEnumerator BossDeathCo()
    {
        data.ChangeEnemyAnimation(anim, "Phantom_Vanish");

        CameraController.instance.BossCam(0);

        BossTrigger.instance.gameObject.GetComponent<Collider2D>().enabled = false;
        BossTrigger.instance.ChangeAnimationHpBar("BossCanvasFadeOut");

        yield return new WaitForSeconds(0.27f);

        winObjects.gameObject.SetActive(true);
        winObjects.transform.SetParent(null);

        yield return new WaitForSeconds(0.72f);

        BossTrigger.instance.anim.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StartChangePositionCo()
    {
        StartCoroutine(ChangePositionCo());
    }
    IEnumerator ChangePositionCo()
    {
        if (currentBossHp > 0)
        {
            data.ChangeEnemyAnimation(anim, "Phantom_Vanish");

            yield return new WaitForSeconds(0.27f);

            anim.gameObject.SetActive(false);
            transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

            yield return new WaitForSeconds(0.72f);

            anim.gameObject.SetActive(true);
            data.ChangeEnemyAnimation(anim, "Phantom_Appears");
        }
    }

    public void ShotFireball()
    {
        if(anim.gameObject.activeSelf)
        {
            Instantiate(fireball, firepoint.position, Quaternion.identity);
        }
        else
        {
            return;
        }
    }
}
