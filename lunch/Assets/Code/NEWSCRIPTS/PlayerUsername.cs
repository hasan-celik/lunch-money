using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUsername : MonoBehaviour
{
    public static PlayerUsername instance;
    
    [SerializeField]private TMP_Text playerNameText;
    public string playername;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişse bile bu objeyi yok etme
        }
        else
        {
            Destroy(gameObject); // Zaten varsa, yeni oluşturulanı yok et
            return;
        }

    }

    void Update()
    {
        if (playerNameText != null)
        {
            playername = playerNameText.text;
        }
    }
}
