using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Metroid2.1 Couse/Enemy")]
public class EnemySO : ScriptableObject
{
    [HideInInspector]public string currentAnimation;

    [Header("Bosses")]
    public bool phantom;
    public string bossName;
    public int fase2, fase3;

    [Header("Mobs")]
    public bool crab;
    public bool flyer;

    [Header("Stats")]
    public int maxHP;
    public int damage;
    public float speed, jumpForce;
    public float attackRange, attackSpeed;

    public void ChangeEnemyAnimation(Animator anim, string newAnimation)
    {
        if(currentAnimation == newAnimation)
        {
            return;
        }
        anim.Play(newAnimation);
        currentAnimation = newAnimation;
    }
}
