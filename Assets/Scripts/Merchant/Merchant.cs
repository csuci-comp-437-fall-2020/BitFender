using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Merchant : MonoBehaviour
{
    public GameObject shopMenuPrefab;
    private GameObject shopMenu;

    public float talkRange = 1.5f;
    public LayerMask player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, talkRange, player);
        if(hit != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                if(shopMenu)
                {
                    Debug.Log("Found thing");
                    shopMenu.SetActive(true);
                }
                else
                {
                    shopMenu = Instantiate(shopMenuPrefab);
                }
            }
        }
    }
}
