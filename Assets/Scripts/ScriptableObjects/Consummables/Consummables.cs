using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

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
    public AnimatorController animator;
}
