using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    private Vector2 startPosition;
    public const float distance = 5f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if(Vector2.Distance(startPosition, transform.position) >= distance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Wall" || hit.gameObject.tag == "Door")
        {
            Destroy(gameObject);
        }
        else if (hit.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            if(hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                hit.gameObject.GetComponent<Player>().GetDamaged(damage);
            }
            Destroy(gameObject);
            
        }
        else if(hit.gameObject.tag == "Enemy" && gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<EnemyBehavior>().GetDamaged(damage);
            Destroy(gameObject);
        }
    }

}
