using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Health health;
    public int maxHealth;
    public int curMaxNumOfHearts;
    public float startingHealth;
}