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
    
    [SerializeField] private TMP_Text copyInfoText;

    
    private string lobbyName;

    private bool isLobbyNameVisible = false;

    private void Update()
    {
        lobbyName = isLobbyNameVisible? PhotonNetwork.CurrentRoom.Name : "*********";
        lobbyNameText.text = $"Lobby : {lobbyName}";
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

    public void HideButton()
    {
        isLobbyNameVisible = !isLobbyNameVisible;
    }

    public void CoppyButton()
    {
        GUIUtility.systemCopyBuffer = PhotonNetwork.CurrentRoom.Name;
        
        copyInfoText.text = "Room name copied!";
        copyInfoText.gameObject.SetActive(true);
        
        StartCoroutine(HideCopyInfo());
    }
    
    
    private IEnumerator HideCopyInfo()
    {
        yield return new WaitForSeconds(1f);
        copyInfoText.gameObject.SetActive(false);
    }

}
