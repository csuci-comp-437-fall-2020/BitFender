using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            int damage = transform.parent.gameObject.GetComponent<EnemyBehavior>().enemy.damage;
            collider.gameObject.GetComponent<Player>().GetDamaged(damage);
        }
    }
}
