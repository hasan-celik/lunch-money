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
    #region quickPlay

    public void QuickPlay()
    {
        if (PhotonNetwork.CountOfRooms > 0)
        {
            PhotonNetwork.JoinRandomRoom(); // Açık odalardan birine katılmaya çalış
        }
        else
        {
            CreateRandomRoom(); // Hiç oda yoksa yeni oda oluştur
        }
    }

// Eğer boş oda bulunamazsa yeni oda aç
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRandomRoom();
    }

// Rastgele oda oluştur
    void CreateRandomRoom()
    {
        string randomRoomName = "Room_" + Random.Range(1000, 9999); // Rastgele oda ismi oluştur
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 20, IsVisible = true, IsOpen = true };
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }


    #endregion
    
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;

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
    }
}