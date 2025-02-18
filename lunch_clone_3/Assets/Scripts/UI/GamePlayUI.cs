using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _miniMap;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button closeMapButton;
    private bool isMapOpen = false;
    
    [SerializeField] private Button exitButton;
    [SerializeField] private Button exit2Button;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button restart2Button;
    
    
    private void Start()
    {
        exitButton.onClick.AddListener(delegate { Application.Quit(); });
        exit2Button.onClick.AddListener(delegate { Application.Quit(); });
        
        
        restartButton.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            if (Time.timeScale == 1)
            {
                PhotonNetwork.LoadLevel("GamePlay");
            }
        });
        restart2Button.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            if (Time.timeScale == 1)
            {
                PhotonNetwork.LoadLevel("GamePlay");
            }
        });
        
        
        if (Application.platform == RuntimePlatform.Android)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);
        }
        
        mapButton.onClick.AddListener(() =>
        {
            isMapOpen = true;
        });
        
        closeMapButton.onClick.AddListener(() =>
        {
            isMapOpen = false;
        });
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            isMapOpen = !isMapOpen;
        }

        if (isMapOpen)
        {
            _map.SetActive(true);
            _miniMap.SetActive(false);
        }
        else
        {
            _map.SetActive(false);
            _miniMap.SetActive(true);
        }
    }
}
