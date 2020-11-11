using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Health")]

public class Health : ScriptableObject
{
    [HideInInspector]
    public int currentHealth;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
}
