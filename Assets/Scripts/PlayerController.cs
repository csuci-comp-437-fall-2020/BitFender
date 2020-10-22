using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement")] 
    public float speed = 500f;
    private Rigidbody2D _body;

    void Start()
    {
        Application.targetFrameRate = 60;

        _body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = -Vector2.left;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = -Vector2.up;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
        }
        _body.velocity = moveDirection * speed * Time.deltaTime;
    }
}
