using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamerSwitching : MonoBehaviour
{

    public Camera playerCamera;
    public Camera awayCamera;
    public Canvas mainScreen;
    public Canvas settingsScreen;
    public GameObject lineManager;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera.enabled = true;
        awayCamera.enabled = false;
        settingsScreen.enabled = false;
        mainScreen.enabled = true;
        lineManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClick()
    {
        playerCamera.enabled = false;
        awayCamera.enabled = true;
        lineManager.SetActive(true);
    }

    public void SettingsButtonClick()
    {
        settingsScreen.enabled = true;
        mainScreen.enabled = false;
    }

    public void SettingsBackButtonClick()
    {
        settingsScreen.enabled = false;
        mainScreen.enabled = true;
    }

}
