using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    Rigidbody2D rb;
    public float fireballSpeed;
    public int damage;
    public GameObject impactEffect;
    PlayerHealthController player;
    void Start()
    {
        player = PlayerHealthController.instance;

        rb = GetComponent<Rigidbody2D>();

        Vector3 direction = transform.position - player.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void FixedUpdate()
    {
        rb.velocity = -transform.right * fireballSpeed;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.DealDamageToPlayer(damage);
        }
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
