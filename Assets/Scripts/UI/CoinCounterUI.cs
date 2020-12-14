using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterUI : MonoBehaviour
{
    public PlayerInventory inventory;

    private Text counter;

    void Start()
    {
        counter = transform.GetChild(0).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        counter.text = "x " + inventory.bitCoin.ToString();
    }
}
