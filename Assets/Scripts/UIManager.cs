﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject restartDialog;
    public Animator dialog;
    



    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        
           dialog.SetBool("isHidden", false);
    }
    public void CloseSettings()
    {

        dialog.SetBool("isHidden", true);
    }



}
