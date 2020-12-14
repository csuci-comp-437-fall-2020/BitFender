using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerInventory")]

public class PlayerInventory : ScriptableObject
{
    public int bitCoin;

    public PowerUp[] currentPowerUps = new PowerUp[6];
}
