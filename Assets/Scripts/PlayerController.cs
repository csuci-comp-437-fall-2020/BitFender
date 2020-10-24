using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Movement")] 
    public float speed = 500f;
    private Rigidbody2D _body;
    private int direction;
    //0 = up, 1 = down, 2 = left, 3 = right


    [Header ("Shooting")]
    public float bulletSpeed = 700f;
    public Rigidbody2D bullet;

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
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = -Vector2.left;
            direction = 3;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = -Vector2.up;
            direction = 1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            direction = 2;
        }
        _body.velocity = moveDirection * speed * Time.deltaTime;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
        switch(direction)
        {
            case 0:
                bulletClone.velocity = Vector2.up * bulletSpeed * Time.deltaTime;
                break;
            case 1:
                bulletClone.velocity = -Vector2.up * bulletSpeed * Time.deltaTime;
                break;
            case 2:
                bulletClone.velocity = Vector2.left * bulletSpeed * Time.deltaTime;
                break;
            case 3:
                bulletClone.velocity = -Vector2.left * bulletSpeed * Time.deltaTime;
                break;
        }
        
    }
}
