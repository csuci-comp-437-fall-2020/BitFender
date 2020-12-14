using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject skillSlotPrefab;

    private const int NUM_OF_POWER_UPS = 6;

    private GameObject[] skillSlots = new GameObject[NUM_OF_POWER_UPS];
    public Player player;

    private int unoccupiedIndex = 0;

    void Start()
    {
        for(int i = 0; i < player.GetComponent<Player>().inventory.currentPowerUps.Length; i++)
        {
            player.GetComponent<Player>().inventory.currentPowerUps[i] = null;
        }
    }

    void Update()
    {
         for(int i = 0; i < player.GetComponent<Player>().inventory.currentPowerUps.Length; i++)
        {
            if(player.GetComponent<Player>().inventory.currentPowerUps[i] != null && player.GetComponent<Player>().inventory.currentPowerUps[i].bought)
            {
                UpdateSkills(player.GetComponent<Player>().inventory.currentPowerUps[i]);
                ActivateBoughtSkill(player.GetComponent<Player>().inventory.currentPowerUps[i]);
                player.GetComponent<Player>().inventory.currentPowerUps[i].bought = false;
            }
        }
    }

    public void UpdateSkills(PowerUp powerUp)
    {
        for(int i = 0; i < NUM_OF_POWER_UPS; i++)
        {
            if(skillSlots[i] == null)
            {
                CreateNewSkillSlot(powerUp);
                break;
            }
            else if(skillSlots[i].name == powerUp.name)
            {
                skillSlots[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = powerUp.numOfStacks.ToString();
                break;
            }
        }
    }

    private void CreateNewSkillSlot(PowerUp powerUp)
    {
        GameObject skillSlot = Instantiate(skillSlotPrefab);
        
        skillSlot.name = powerUp.name;
        skillSlot.transform.GetChild(1).GetComponent<Image>().sprite = powerUp.sprite;
        skillSlot.transform.SetParent(gameObject.transform.GetChild(1).transform);
        skillSlots[unoccupiedIndex] = skillSlot;

        if(!powerUp.stackable)
        {
            skillSlot.transform.GetChild(2).gameObject.SetActive(false);
        }

        unoccupiedIndex++;
    }

    private void ActivateBoughtSkill(PowerUp powerUp)
    {
        switch (powerUp.type) {
                case PowerUp.TYPE.DAMAGE_BOOST:
                    player.GetComponent<Shooting> ().damage++;
                    break;
                case PowerUp.TYPE.MAX_HEALTH_UP:
                    player.GetComponent<Player> ().IncreaseMaxHealth ();
                    break;
                case PowerUp.TYPE.SHIELD:
                    player.GetComponent<Player> ().hasShield = true;
                    break;
                case PowerUp.TYPE.MOVEMENT_UP:
                    player.GetComponent<PlayerController> ().speed += 10f;
                    break;
                case PowerUp.TYPE.BULLET_COUNT_UP:
                    player.GetComponent<PlayerController> ().numOfBulletChained++;
                    break;
                case PowerUp.TYPE.SHOTGUN:
                    player.GetComponent<PlayerController> ().shootType = 1;
                    break;

            }
    }
}
