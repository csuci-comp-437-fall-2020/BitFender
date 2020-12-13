using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Consummable")]

public class Consummables : ScriptableObject
{
    public enum TYPE
    {
        BITCOIN,
        HEALTH
    }

    public TYPE type;

    public Sprite sprite;
    public RuntimeAnimatorController animator;
}
