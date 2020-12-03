using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health health;
    public GameObject heartUIIcon;
    public int curMaxHearts;
    public const int MAX_NUM_OF_HEARTS = 10;

    private GameObject[] heartIcons = new GameObject[MAX_NUM_OF_HEARTS];
    //[HideInInspector]
    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        curMaxHearts = player.curMaxNumOfHearts;
        CreateHeartIcons();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            SetHealthUI();
        }
    }

    public void CreateHeartIcons()
    {
        if(heartUIIcon)
        {
            for (int i = 0; i < curMaxHearts; i++)
            {
                GameObject newHeartIcon = Instantiate(heartUIIcon);
                newHeartIcon.name = "HeartIcon_" + i;
                newHeartIcon.GetComponent<Image>().sprite = health.fullHeart;
                newHeartIcon.transform.SetParent(gameObject.transform.GetChild(0).transform);
                heartIcons[i] = newHeartIcon;
            }
        }
    }

    public void AddHeartIcon()
    {
        curMaxHearts++;
        if(heartUIIcon && curMaxHearts <= MAX_NUM_OF_HEARTS)
        {
            GameObject newHeartIcon = Instantiate(heartUIIcon);
            newHeartIcon.name = "HeartIcon_" + (curMaxHearts - 1);
            newHeartIcon.GetComponent<Image>().sprite = health.fullHeart;
            newHeartIcon.transform.SetParent(gameObject.transform.GetChild(0).transform);
            heartIcons[curMaxHearts - 1] = newHeartIcon;
        }
        SetHealthUI();
    }

    public void Heal()
    {
        health.currentHealth++;
        if(health.currentHealth > curMaxHearts)
        {
            health.currentHealth = curMaxHearts;
        }
        heartIcons[health.currentHealth - 1].GetComponent<Image>().sprite = health.fullHeart;
    }

    private void SetHealthUI()
    {
        if(health.currentHealth < curMaxHearts)
        {
            for(int i = curMaxHearts - 1; i >= health.currentHealth; i--)
            {
                Debug.Log(i);
                Debug.Log(heartIcons[i]);
                heartIcons[i].GetComponent<Image>().sprite = health.emptyHeart;
            }
        }
    }
}
