using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public PowerUp powerUp;
    public Text price;

    public PlayerInventory inventory;

    private Button purchaseButton;

    void Start()
    {
        purchaseButton = GetComponent<Button>();
    }

    public void Purchase()
    {
        if(inventory.bitCoin >= powerUp.price)
        {
            inventory.bitCoin -= powerUp.price;
            SkillUIUpdate();
            powerUp.bought = true;
            purchaseButton.interactable = false;
        }
    }

    private void SkillUIUpdate()
    {
        for(int i = 0; i < inventory.currentPowerUps.Length; i++)
        {
            if(inventory.currentPowerUps[i] == null)
            {
                inventory.currentPowerUps[i] = powerUp;
                inventory.currentPowerUps[i].numOfStacks++;
                break;
            }
            else if (powerUp.name == inventory.currentPowerUps[i].name) {
                inventory.currentPowerUps[i].numOfStacks++;
                break;
            }
        }
    }
}
