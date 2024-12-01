using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject _map;
    [SerializeField] private GameObject _miniMap;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button closeMapButton;
    private bool isMapOpen = false;

    private void Start()
    {
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
