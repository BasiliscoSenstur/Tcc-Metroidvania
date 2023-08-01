using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject impactEffect;
    [SerializeField] float shotSpeed, shotDir;
    bool UpShot;
    public int damage = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shotDir = PlayerController.instance.transform.localScale.x;

        if (PlayerController.instance.currentState == PlayerController.instance.shootUpState)
        {
            UpShot = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (UpShot)
        {
            rb.velocity = new Vector2(rb.velocity.x, shotSpeed);
        }
        else
        {
            rb.velocity = new Vector2(shotSpeed * shotDir, rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController script;
            script = other.GetComponent<EnemyController>();
            script.TakeDamage(damage);
        }
        if (other.CompareTag("Boss"))
        {
            BossController script;
            script = other.GetComponent<BossController>();
            script.DealDamageBoss(damage);
        }
        DestroyProjectile();
        AudioController.instance.PlaySfxAdjusted(8);
    }
    
    void DestroyProjectile()
    {
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
