using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public GameObject slot;
    public const int MAX_SLOTS = 9;

    [SerializeField]
    private PowerUp[] possiblePowerUps;

    private PowerUp[] items = new PowerUp[MAX_SLOTS];
    private GameObject[] merchantInventory = new GameObject[MAX_SLOTS];
    private Image[] powerUpImages = new Image[MAX_SLOTS];

    // Start is called before the first frame update
    void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        int numOfItemsToHave = Random.Range(1, 10);
        for (int i = 0; i < numOfItemsToHave; i++)
        {
            int powerUpToAdd = Random.Range(0, possiblePowerUps.Length);
            GameObject newSlot = Instantiate(slot);
            newSlot.name = "ShopSlot_" + i;
            newSlot.GetComponent<Slot>().powerUp = possiblePowerUps[powerUpToAdd];
            newSlot.GetComponent<Slot>().price.text = "x " + possiblePowerUps[powerUpToAdd].price;
            newSlot.transform.GetChild(1).GetComponent<Image>().sprite = possiblePowerUps[powerUpToAdd].sprite;

            newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform.GetChild(0));
            merchantInventory[i] = newSlot;
            items[i] = newSlot.GetComponent<Slot>().powerUp;

            powerUpImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
        }
    }

    public void ExitMenu()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
