using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

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

        // Oyuncunun rengi daha önce kaydedildiyse, geri yükle
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("PlayerColor", out object colorData))
        {
            float[] colorArray = (float[])colorData;
            ApplyColorToPlayer(new Color(colorArray[0], colorArray[1], colorArray[2], colorArray[3]));
        }
    }

    private void OnButtonClicked(int index)
    {
        Color selectedColor = colors[index];

        foreach (var player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                // Rengi kaydet
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                hash["PlayerColor"] = new float[] { selectedColor.r, selectedColor.g, selectedColor.b, selectedColor.a };
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

                // RPC ile tüm oyunculara bildir
                player.GetComponent<PhotonView>().RPC("setColor", RpcTarget.AllBuffered, selectedColor.r, selectedColor.g, selectedColor.b, selectedColor.a);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Master Client isen, yeni oyuncuya tüm oyuncuların renklerini gönder
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.CustomProperties.TryGetValue("PlayerColor", out object colorData))
                {
                    float[] colorArray = (float[])colorData;
                    photonView.RPC("setColor", newPlayer, colorArray[0], colorArray[1], colorArray[2], colorArray[3]);
                }
            }
        }
    }

    private void ApplyColorToPlayer(Color color)
    {
        foreach (var player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                player.GetComponent<PhotonView>().RPC("setColor", RpcTarget.AllBuffered, color.r, color.g, color.b, color.a);
            }
        }
    }
}
