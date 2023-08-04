using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamageToPlayer(1000);
        }
    }
}
