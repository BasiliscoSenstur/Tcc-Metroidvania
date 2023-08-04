using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    PlayerController player;

    [Header("Health")]
    public GameObject deathEffect;
    public int maxHP;
    public int currentHP;
    public float invencibilityCounter, invencibilityTime;

    [Header("Flash Sprites")]
    public SpriteRenderer[] playerSprites;
    public float flashCounter, flashLenght;

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
        currentHP = maxHP;
    }
    void Start()
    {

        player = PlayerController.instance;
    }

    void Update()
    {
        if (invencibilityCounter > 0)
        {
            invencibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = !sr.enabled;
                }
                flashCounter = flashLenght;
            }

            if(invencibilityCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprites)
                {
                    sr.enabled = true;
                }
                flashCounter = 0;
            }
        }
    }

    public void DealDamageToPlayer(int damage)
    {
        if (invencibilityCounter <= 0)
        {
            currentHP -= damage;
            AudioController.instance.PlaySfx(0);
            UIController.instance.UpdateHealthDisplay();
            invencibilityCounter = invencibilityTime;
            flashCounter = flashLenght;

            if (currentHP <= 0)
            {
                currentHP = 0;
                invencibilityCounter = 0;
                AudioController.instance.PlaySfx(10);
                Instantiate(deathEffect, transform.position, transform.rotation);
                PlayerSpawnController.instance.StartRespawnPlayerCo();
            }
        }
    }

    public void FillHP()
    {
        currentHP = maxHP;
        UIController.instance.UpdateHealthDisplay();
    }

    public void HealHP(int amount)
    {
        currentHP += amount;
        UIController.instance.UpdateHealthDisplay();
    }
}
