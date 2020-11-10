using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    public PowerUp powerUp;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = powerUp.sprite;
    }
    
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            PlayerController player = hit.gameObject.GetComponent<PlayerController>();
            switch(powerUp.type)
            {
                case PowerUp.TYPE.DAMAGE_BOOST:
                    player.damage++;
                    break;
                case PowerUp.TYPE.MAX_HEALTH_UP:
                    player.IncreaseMaxHealth();
                    break;
                case PowerUp.TYPE.SHIELD:
                    //TODO: will do later since I have to coad a following shield and all that cool jazz
                    break;
                case PowerUp.TYPE.MOVEMENT_UP:
                    player.speed += 10f;
                    break;

            }
            Destroy(gameObject);
        }
    }
}
