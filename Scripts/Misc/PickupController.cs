using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupController : MonoBehaviour
{
    PlayerController player;

    [Header("Pick Up")]
    public bool isAbilityPickUp;
    public bool isHealPickUp;
    [SerializeField] GameObject pickupEffect;

    [Header("Health")]
    [SerializeField] int healAmount;

    [Header("Abillities")]
    [SerializeField] bool doubleJump;
    [SerializeField] bool dash, changeMode, dropBomb;

    [SerializeField] TMP_Text pickUpText;
    string abilityToUnlock;

    private void Start()
    {
        player = PlayerController.instance;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAbilityPickUp)
            {
                if (doubleJump)
                {
                    abilityToUnlock = "Double Jump Unlocked";
                    player.canDoubleJump = true;
                }
                if (dash)
                {
                    abilityToUnlock = "Dash Unlocked";
                    player.canDash = true;
                }
                if (changeMode)
                {
                    abilityToUnlock = "Change Mode Unlocked";
                    player.canChangeMode = true;
                }
                if (dropBomb)
                {
                    abilityToUnlock = "Drop Bomb Unlocked";
                    player.canDropBomb = true;
                }

                //Text
                pickUpText.gameObject.SetActive(true);
                pickUpText.text = abilityToUnlock;

                pickUpText.transform.parent.SetParent(null);
                pickUpText.transform.parent.position = transform.position;

                Destroy(pickUpText.transform.parent.gameObject, 5f);
            }
            if (isHealPickUp)
            {
                PlayerHealthController.instance.HealHP(healAmount);
            }
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
