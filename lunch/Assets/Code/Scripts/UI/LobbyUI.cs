using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private Button _button;

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
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("GamePlay");
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            _button.GetComponent<TMP_Text>().text = "Ready";
        }
        
        if (PhotonNetwork.IsMasterClient)
        {
            _button.GetComponentInChildren<TMP_Text>().text = "Start";
            
            _button.onClick.AddListener(() =>
            {
                
            });
        }
    }
}
