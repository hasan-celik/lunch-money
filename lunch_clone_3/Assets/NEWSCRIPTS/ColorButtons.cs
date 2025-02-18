using System;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtons : MonoBehaviourPunCallbacks
{
    public Button[] buttons;
    public Color[] colors;
    
    private List<GameObject> players;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        // Buton renklerini kaydet
        colors = buttons.Select(b => b.colors.normalColor).ToArray();

        // Butonların tıklama eventlerini oluştur
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Lambda expression içindeki scope için gerekli
            buttons[index].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnButtonClicked(int index)
    {
        foreach (var player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                player.GetComponent<PhotonView>().RPC("setColor", RpcTarget.All, colors[index].r, colors[index].g, colors[index].b, colors[index].a);
            }
        }
    }
    
}