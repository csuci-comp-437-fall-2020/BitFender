using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    [Header("PowerUps")]
    public PowerUpList powerUpList;
    private Transform powerUpSpawnPoint;
    private bool dropped = false;

    [Header("Enemies")]
    public GameObject[] creatableEnemies;
    [HideInInspector]
    public List<GameObject> populatedEnemies = new List<GameObject>();
    
    [Header("Doors")]
    private Door[] doors;
    private Collider2D _groundCollider;
    private CinemachineConfiner _camera;

    [HideInInspector]
    public bool playerInRoom;
    
    void Awake()
    {
        doors = GetComponentsInChildren<Door>();
        foreach(Transform child in transform)
        {
            if(child.tag == "Ground")
            {
                _groundCollider = child.GetComponent<Collider2D>();
            }
            else if(child.tag == "Enemy")
            {
                populatedEnemies.Add(child.gameObject);
            }
            else if(child.tag == "SpawnPoint")
            {
                powerUpSpawnPoint = child.transform;
            }
        }

        _camera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineConfiner>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraBounds();

        if(populatedEnemies.Count <= 0)
        {
            foreach(Door door in doors)
            {
                door.OpenDoor();
            }
            if(!dropped)
            {
                DropPowerUp();
            }
        }
    }

    private void SetCameraBounds()
    {
        if(playerInRoom)
        {
            _camera.m_BoundingShape2D = _groundCollider;
        }
    }

    private void DropPowerUp()
    {
        int random = Random.Range(0, powerUpList.powerUps.Length - 1);

        dropped = true;

        Instantiate(powerUpList.powerUps[random], powerUpSpawnPoint);
    }
}
