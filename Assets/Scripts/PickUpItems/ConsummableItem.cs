using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsummableItem : MonoBehaviour
{
    public Consummables item;

    private const float speed = 10f;
    private const int RANDOM_MAX = 8;
    private Vector2 direction;
    private Rigidbody2D _body;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();

        GetComponent<SpriteRenderer>().sprite = item.sprite;
        GetComponent<Animator>().runtimeAnimatorController = item.animator;

        direction = SetTravelDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _body.velocity = direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            switch(item.type)
            {
                case Consummables.TYPE.BITCOIN:
                    player.inventory.bitCoin++;
                    break;
                case Consummables.TYPE.HEALTH:
                    player.Heal();
                    break;
            }
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            direction = Vector2.zero;
        }
    }

    private Vector2 SetTravelDirection()
    {
        Vector2 wayward = Vector2.zero;
        int random = Random.Range(0, RANDOM_MAX);
        switch(random)
        {
            case 0:
                wayward = Vector2.up;
                break;
            case 1:
                wayward = new Vector2(1, 1);
                break;
            case 2:
                wayward = -Vector2.left;
                break;
            case 3:
                wayward = new Vector2(1, -1);
                break;
            case 4:
                wayward = -Vector2.up;
                break;
            case 5:
                wayward = new Vector2(-1, -1);
                break;
            case 6:
                wayward = Vector2.left;
                break;
            case 7:
                wayward = new Vector2(-1, 1);
                break;
        }
        return wayward;
    }

}
