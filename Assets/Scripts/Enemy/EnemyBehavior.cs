using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Enemy enemy;

    public LayerMask whatIsPlayer;
    public Rigidbody2D bullet;

    [Header("Droppables")]
    public GameObject bitCoin;
    public GameObject healthDrop;

    private float attackCounter;

    [HideInInspector]
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = enemy.sprite;

        attackCounter = enemy.attackSpeed;
        currentHealth = enemy.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Death();
    }

    public void GetDamaged(int damage)
    {
        currentHealth -= damage;
    }

    private void Attack()
    {
        switch (enemy.enemyType)
        {
            case Enemy.ENEMY_TYPE.STATIONARY:
                if(attackCounter <= 0)
                {
                    Collider2D hit = Physics2D.OverlapCircle(transform.position, enemy.detectRange, whatIsPlayer);
                    if(hit != null)
                    {
                        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
                        bulletClone.gameObject.tag = "Enemy";
                        bulletClone.GetComponent<Bullet>().damage = enemy.damage;
                        Vector2 direction = new Vector2(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y);
                        bulletClone.velocity = direction * enemy.bulletSpeed * Time.deltaTime;

                        attackCounter = enemy.attackSpeed;
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
            DropItem();
            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        Vector3 itemSpawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float chanceToDrop = Random.Range(0f, 1f);
        if(chanceToDrop >= 0.5f)
        {
            int whichItem = Random.Range(0, 2);
            switch(whichItem)
            {
                case 0:
                    //Drop bitCoins
                    int numOfCoins = Random.Range(1, 9);
                    for (int i = 0; i < numOfCoins; i++)
                    {
                        Instantiate(bitCoin, itemSpawnPoint, transform.rotation);
                    }
                    break;
                case 1:
                    //Drop Health
                    Instantiate(healthDrop, itemSpawnPoint, transform.rotation);
                    break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.detectRange);
    }
}
