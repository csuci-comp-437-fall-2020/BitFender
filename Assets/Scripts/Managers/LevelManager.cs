using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    public GameObject restartDialogue;
    public Text restartText;

    void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.childCount > 0)
            {
                foreach(Transform thing in child.transform)
                {
                    if(thing.tag == "Enemy")
                    {
                        enemies.Add(thing.gameObject);
                    }
                }
            }
        }
    }

    void Update()
    {
        if(enemies.Count <= 0)
        {
            restartDialogue.SetActive(true);
            Time.timeScale = 0f;

            restartText.text = "You've Defended the System";
        }
    }
}
