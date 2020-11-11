﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [Header ("Health")]
    public HealthUI healthUIPrefab;
    private HealthUI healthUI;
    
    // NB Added Following Codes Delete or edit if wrong
    [Header("Restart")]
    public GameObject restartDialog;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        health.currentHealth = maxHealth;
        healthUI = Instantiate(healthUIPrefab);

        //NB Added Following Codes Delete or edit if wrong
        restartDialog.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    public void GetDamaged(int damage)
    {
        health.currentHealth -= damage;
    }

    public void IncreaseMaxHealth()
    {
        maxHealth++;
        healthUI.GetComponent<HealthUI>().AddHeartIcon();
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
