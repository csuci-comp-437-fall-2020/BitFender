using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (hit.gameObject.tag == "Player" && gameObject.tag == "Enemy")
        {
            hit.gameObject.GetComponent<PlayerController>().currentHealth--;
            Destroy(gameObject);
        }
        else if(hit.gameObject.tag == "Enemy" && gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<EnemyBehavior>().currentHealth--;
            Destroy(gameObject);
        }
    }
}
