using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class LobbyInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyNameText;
    [SerializeField] private TMP_Text isLobbyOpenText;
    [SerializeField] private TMP_Text playerCountText;


    private void Update()
    {
        lobbyNameText.text = $"Lobby : {PhotonNetwork.CurrentRoom.Name}";
        isLobbyOpenText.text = PhotonNetwork.CurrentRoom.IsOpen?"Public Lobby":"Private Lobby";
        playerCountText.text = $"Players : {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

    public void LogCurrentRoomInfo()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            Debug.Log($"Odaya Katıldın: {PhotonNetwork.CurrentRoom.Name}");
            Debug.Log($"Oyuncu Sayısı: {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}");
            Debug.Log($"Gizli mi?: {PhotonNetwork.CurrentRoom.IsVisible}");
            Debug.Log($"Açık mı?: {PhotonNetwork.CurrentRoom.IsOpen}");
        }
        else
        {
            Debug.Log("Şu anda bir odada değilsin.");
        }
    }
}
