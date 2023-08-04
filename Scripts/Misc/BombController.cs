using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] GameObject explision;
    float timer, explosionRange = 1.5f;
    [SerializeField] LayerMask whatIsDestructible, whatIsEnemy;

    void Start()
    {
        timer = 1.5f;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Destroy(gameObject);
            Instantiate(explision, transform.position, transform.rotation);
            AudioController.instance.PlaySfxAdjusted(3);

            Collider2D[] ObjectsToDemage = Physics2D.OverlapCircleAll
                (transform.position, explosionRange, whatIsDestructible);

            if (ObjectsToDemage.Length > 0)
            {
                foreach (Collider2D other in ObjectsToDemage)
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
}
