using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp")]

public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public Sprite sprite;
    public int price;
    public bool stackable;

    public enum TYPE 
    {
        DAMAGE_BOOST, 
        MAX_HEALTH_UP, 
        SHIELD, 
        MOVEMENT_UP,
        BULLET_COUNT_UP,
        SHOTGUN
    }

    public TYPE type;
    public int numOfStacks;

    public bool bought = false;
}
