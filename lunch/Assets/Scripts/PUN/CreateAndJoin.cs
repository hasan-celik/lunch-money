using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Firebase.Auth;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;
    public TMP_Text playerListText; // UI'da oyuncu listesini gösterecek Text nesnesi

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(input_Create.text, new RoomOptions() {MaxPlayers = 20, IsVisible = true, IsOpen = true}, 
            TypedLobby.Default, null);
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }

    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GamePlay");
        UpdatePlayerList(); // Odaya girince oyuncu listesini güncelle
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList(); // Yeni biri katıldığında listeyi güncelle
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList(); // Biri ayrıldığında listeyi güncelle
    }

    private void UpdatePlayerList()
    {
        if (playerListText == null) return;

        playerListText.text = "Players in Room:\n";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n"; // Oyuncu adlarını ekliyoruz
        }
    }
}