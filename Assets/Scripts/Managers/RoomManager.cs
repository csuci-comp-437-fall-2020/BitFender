using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] creatableEnemies;
    [HideInInspector]
    public List<GameObject> populatedEnemies = new List<GameObject>();
    
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
            if(child.tag == "Enemy")
            {
                populatedEnemies.Add(child.gameObject);
            }
        }

        _camera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineConfiner>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraBounds();

        if(populatedEnemies.Count == 0)
        {
            foreach(Door door in doors)
            {
                door.OpenDoor();
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
}
