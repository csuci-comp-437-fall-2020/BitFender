using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (hit.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            hit.gameObject.GetComponent<Player>().GetDamaged(damage);
            Destroy(gameObject);
        }
        else if(hit.gameObject.tag == "Enemy" && gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<EnemyBehavior>().GetDamaged(damage);
            Destroy(gameObject);
        }
    }
}
