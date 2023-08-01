using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    Vector2 spawnPoint;

    private void Start()
    {
        spawnPoint = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSpawnController.instance.SetSpawnPoint(spawnPoint);
        }
    }
}
