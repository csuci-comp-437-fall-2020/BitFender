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

    public void Purchase()
    {
        if(inventory.bitCoin >= powerUp.price)
        {
            inventory.bitCoin -= powerUp.price;
        }
    }
}
