using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    public enum TYPE {DAMAGE_BOOST, MAX_HEALTH_UP, SHIELD, MOVEMENT_UP}

    public TYPE type;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            PlayerController player = hit.gameObject.GetComponent<PlayerController>();
            switch(type)
            {
                case TYPE.DAMAGE_BOOST:
                    player.damage++;
                    break;
                case TYPE.MAX_HEALTH_UP:
                    player.IncreaseMaxHealth();
                    break;
                case TYPE.SHIELD:
                    //TODO: will do later since I have to coad a following shield and all that cool jazz
                    break;
                case TYPE.MOVEMENT_UP:
                    player.speed += 10f;
                    break;

            }
            Destroy(gameObject);
        }
    }
}
