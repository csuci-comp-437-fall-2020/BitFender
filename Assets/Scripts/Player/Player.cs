using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : Character
{
    [Header ("Health")]
    public HealthUI healthUIPrefab;
    private HealthUI healthUI;
    private Collider2D hitbox;

    [Header("Skills")]
    public InventoryUI skillUIPrefab;
    [HideInInspector]
    public InventoryUI skillUI;

    // NB Added Following Codes Delete or edit if wrong
    [Header("Restart")]
    public GameObject restartDialog;

    [Header("Collectables")]
    public PlayerInventory inventory;

    [HideInInspector]
    public bool hasShield;
    private Animator shield;
    private float flashingTime;
    private const float FLASH_TIME = 0.1f;
    private const float MAX_TIME_FOR_FLASH = 0.1f;

    [HideInInspector]
    public GameObject currentRoom;

    private CinemachineVirtualCamera _camera;

    private SpriteRenderer _sprite;

    // Start is called before the first frame update

    void Awake()
    {
        _camera = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        _camera.Follow = transform;
        _camera.LookAt = transform;
        skillUI = Instantiate(skillUIPrefab);
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        health.currentHealth = maxHealth;
        healthUI = Instantiate(healthUIPrefab);

        

        hitbox = transform.GetChild(0).GetComponent<Collider2D>();
        _sprite = GetComponent<SpriteRenderer>();
        flashingTime = MAX_TIME_FOR_FLASH;

        currentRoom.GetComponent<RoomManager>().playerInRoom = true;
        hasShield = false;

        shield = transform.GetChild(1).gameObject.GetComponent<Animator>();

        //NB Added Following Codes Delete or edit if wrong
        restartDialog.SetActive(false);
        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        Shield();
        Death();
    }

    public void GetDamaged(int damage)
    {
        if(hasShield)
        {
            StartCoroutine(DestroyShield());
        }
        else
        {
            health.currentHealth -= damage;
            StartCoroutine(DamageFlash());
        }
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
        healthUI.GetComponent<HealthUI>().AddHeartIcon();
    }

    public void Heal()
    {
        healthUI.GetComponent<HealthUI>().Heal();
    }

    private void Death()
    {
        if(health.currentHealth <= 0)
        {
            //NB Added Following Codes Delete or edit if wrong
            restartDialog.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private IEnumerator DamageFlash()
    {
        while(flashingTime >= 0.0f)
        {
            _sprite.color = Color.red;
            yield return new WaitForSeconds(FLASH_TIME);
            _sprite.color = Color.white;
            flashingTime -= Time.deltaTime;
            yield return new WaitForSeconds(FLASH_TIME);
        }
        flashingTime = MAX_TIME_FOR_FLASH;
    }

    private void Shield()
    {
        if(hasShield)
        {
            shield.gameObject.SetActive(true);
            shield.SetBool("hasShield", true);
        }
    }

    private IEnumerator DestroyShield()
    {
        shield.SetBool("hasShield", false);
        hasShield = false;
        yield return new WaitForSeconds(0.5f);
        shield.gameObject.SetActive(false);
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
