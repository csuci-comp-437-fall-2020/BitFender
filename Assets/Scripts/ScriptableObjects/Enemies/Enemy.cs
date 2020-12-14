using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy")]

public class Enemy : ScriptableObject
{
    public enum ENEMY_TYPE
    {
        STATIONARY,
        WANDERING,
        EXPLODING
    }

    public ENEMY_TYPE enemyType;
    public Sprite sprite;

    [Header("Enemy Attack")]
    public float bulletSpeed;
    public float detectRange;
    public float attackSpeed;
    public int damage;

    [Header ("Health")]
    public int maxHealth;
}
