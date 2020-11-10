using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header ("Movement")] 
    public float speed = 500f;
    private Rigidbody2D _body;
    private int direction;
    //0 = up, 1 = down, 2 = left, 3 = right
    private Animator _animator;


    [Header ("Shooting")]
    public float bulletSpeed = 700f;
    public Rigidbody2D bullet;
    public int damage = 1;
    public int numOfBulletChained = 3;
    private int numOfBulletShot;
    public float maxShootCooldown = 2f; //this is max time between bullet shots
    private float currentShootCooldown;
    public float maxBufferPeriod = 2f; //this is max time between shots to refresh.
    //This is such that the player will not have to shoot all bullets to reset the timer.
    private float currentBufferPeriod;

    [Header ("Health")]
    public int maxHealth;
    public int currentHealth;
    public Image[] healthMeter;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite halfHeart;
    [SerializeField]
    private Sprite emptyHeart;
    

    // NB Added Following Codes Delete or edit if wrong
    [Header("Restart")]
    public GameObject restartDialog;
  


    void Start()
    {
        Application.targetFrameRate = 60;

        _body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        _animator = GetComponent<Animator>();
        _animator.SetFloat("moveX", 0);
        _animator.SetFloat("moveY", -1);

        for (int i = 0; i < healthMeter.Length; i++)
        {
            if(i < maxHealth)
            {
                healthMeter[i].enabled = true;
            }
            else
            {
                healthMeter[i].enabled = false;
            }
        }

        numOfBulletShot = 0;

        //NB Added Following Codes Delete or edit if wrong
        restartDialog.SetActive(false);
        Time.timeScale = 1f;
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = Vector2.zero;
        _animator.SetBool("moving", false);

        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
            direction = 0;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = -Vector2.left;
            direction = 3;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection = -Vector2.up;
            direction = 1;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            direction = 2;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        _body.velocity = moveDirection * speed * Time.deltaTime;

        if(_body.velocity != Vector2.zero)
        {
            _animator.SetBool("moving", true);
        }
    }

    void Update()
    {
        //THIS DOES NOT CHANGE THE VELOCITY OF THE RIGIDBODY. ONLY FACING DIRECTION OF THE SPRITE
        Vector2 moveDirection;
        if(Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector2.up;
            direction = 0;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            moveDirection = -Vector2.left;
            direction = 3;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else if(Input.GetKey(KeyCode.S))
        {
             moveDirection = -Vector2.up;
            direction = 1;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector2.left;
            direction = 2;
            _animator.SetFloat("moveX", moveDirection.x);
            _animator.SetFloat("moveY", moveDirection.y);
        }

        Shoot();
        Death();
    }

    public void GetDamaged(int damage)
    {
        currentHealth -= damage;
        SetHealthUI();
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
        for (int i = 0; i < healthMeter.Length; i++)
        {
            if(i < maxHealth)
            {
                healthMeter[i].enabled = true;
            }
            else
            {
                healthMeter[i].enabled = false;
            }
        }
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        if(currentHealth < maxHealth)
        {
            for(int i = healthMeter.Length - 1; i >= currentHealth; i--)
            {
                healthMeter[i].sprite = emptyHeart;
            }
        }
    }

    private void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentShootCooldown <= 0)
        {
            if(numOfBulletShot < numOfBulletChained)
            {
                Rigidbody2D bulletClone = (Rigidbody2D)Instantiate(bullet, transform.position, transform.rotation);
                bulletClone.gameObject.tag = "Player";
                bulletClone.GetComponent<Bullet>().damage = damage;
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
                numOfBulletShot++;
                currentBufferPeriod = maxBufferPeriod;
            }
            
            if(numOfBulletShot == numOfBulletChained)
            {
                currentShootCooldown = maxShootCooldown;
                currentBufferPeriod = maxBufferPeriod;
                numOfBulletShot = 0;
            }
        }
        else
        {
            currentShootCooldown -= Time.deltaTime;
        }

        if(numOfBulletShot < numOfBulletChained && currentBufferPeriod <= 0)
        {
            currentBufferPeriod = maxBufferPeriod;
            numOfBulletShot = 0;
        }
        else
        {
            currentBufferPeriod -= Time.deltaTime;
        }
    }

    private void Death()
    {
        if(currentHealth <= 0)
        {
            //NB Added Following Codes Delete or edit if wrong
            restartDialog.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //NB Added Following Codes Delete or edit if wrong

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
