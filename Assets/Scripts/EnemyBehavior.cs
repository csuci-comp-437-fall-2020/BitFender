using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public enum ENEMY_TYPE {STATIONARY, BASIC, BASHER}
    //We'll add more as we think up more.

    [Header("Enemy Parameters")]
    public ENEMY_TYPE enemyType;
    public LayerMask whatIsPlayer;
    public Rigidbody2D bullet;

    [Header ("Enemy Attack")]
    public float bulletSpeed = 700f;
    public float detectRange = 5f;
    public float attackSpeed = 1f;
    private float attackCounter;

    [Header ("Health")]
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        attackCounter = attackSpeed;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Death();
    }

    private void Attack()
    {
        switch (enemyType)
        {
            case ENEMY_TYPE.STATIONARY:
                if(attackCounter <= 0)
                {
                    Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRange, whatIsPlayer);
                    if(hit != null)
                    {
                        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
                        bulletClone.gameObject.tag = "Enemy";
                        Vector2 direction = new Vector2(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y);
                        bulletClone.velocity = direction * bulletSpeed * Time.deltaTime;

                        attackCounter = attackSpeed;
                    }
                }
                else
                {
                    attackCounter -= Time.deltaTime;
                }
                
                break;
        }
    }

    private void Death()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
