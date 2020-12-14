using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffect : MonoBehaviour {
    public PowerUp powerUp;

    void Start () {
        GetComponent<SpriteRenderer> ().sprite = powerUp.sprite;
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D (Collider2D hit) {
        if (hit.gameObject.tag == "Player" && hit.gameObject.layer == LayerMask.NameToLayer ("PlayerHitBox")) {
            GameObject player = hit.transform.parent.gameObject;
            switch (powerUp.type) {
                case PowerUp.TYPE.DAMAGE_BOOST:
                    player.GetComponent<Shooting> ().damage++;
                    IncreaseSkillCount (player);
                    break;
                case PowerUp.TYPE.MAX_HEALTH_UP:
                    player.GetComponent<Player> ().IncreaseMaxHealth ();
                    IncreaseSkillCount (player);
                    break;
                case PowerUp.TYPE.SHIELD:
                    player.GetComponent<Player> ().hasShield = true;
                    break;
                case PowerUp.TYPE.MOVEMENT_UP:
                    player.GetComponent<PlayerController> ().speed += 10f;
                    IncreaseSkillCount (player);
                    break;
                case PowerUp.TYPE.BULLET_COUNT_UP:
                    player.GetComponent<PlayerController> ().numOfBulletChained++;
                    IncreaseSkillCount (player);
                    break;
                case PowerUp.TYPE.SHOTGUN:
                    player.GetComponent<PlayerController> ().shootType = 1;
                    IncreaseSkillCount (player);
                    break;

            }
            Destroy (gameObject);
        }
    }

    private void IncreaseSkillCount (GameObject player) {
        for (int i = 0; i < player.GetComponent<Player> ().inventory.currentPowerUps.Length; i++) {
            if (player.GetComponent<Player> ().inventory.currentPowerUps[i] == null) {
                player.GetComponent<Player> ().inventory.currentPowerUps[i] = powerUp;
                player.GetComponent<Player> ().inventory.currentPowerUps[i].numOfStacks++;
                break;
            } else if (powerUp.name == player.GetComponent<Player> ().inventory.currentPowerUps[i].name) {
                player.GetComponent<Player> ().inventory.currentPowerUps[i].numOfStacks++;
                break;
            }
        }
        player.GetComponent<Player>().skillUI.UpdateSkills(powerUp);
    }
}