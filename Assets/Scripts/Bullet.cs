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
        else if (hit.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
