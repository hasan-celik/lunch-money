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
    public TMP_InputField input_MaxPlayers;
    public Toggle input_isRoomVisible;
    public Slider input_MaxPlayersSlider;

    public TMP_Text IsroomVisibleText;
    
    public int maxPlayersInRoom;
    public bool isRoomVisible = true;

    public void CreateRoom()
    {
        // Değerleri burada çekerek null hatasını önlüyoruz.
        if (!int.TryParse(input_MaxPlayers.text, out maxPlayersInRoom))
        {
            maxPlayersInRoom = 20; // Varsayılan bir değer belirle (örneğin 4 oyuncu)
        }

        PhotonNetwork.CreateRoom(input_Create.text, new RoomOptions()
        {
            MaxPlayers = (byte)maxPlayersInRoom, // MaxPlayers bir byte olmalı!
            IsVisible = isRoomVisible,
            IsOpen = true
        }, TypedLobby.Default, null);
    }

    public void ToggleClick()
    {
        isRoomVisible = !isRoomVisible;
        if (isRoomVisible)
        {
            IsroomVisibleText.text = "Public";
        }
        else
        {
            IsroomVisibleText.text = "Private";
        }
    }

    public void inputMaxPlayers_OnValueChanged_Textbox()
    {
        maxPlayersInRoom = int.Parse(input_MaxPlayers.text);
        input_MaxPlayersSlider.value = maxPlayersInRoom;
    }

    public void inputMaxPlayers_OnValueChanged_Slider()
    {
        maxPlayersInRoom = (int)input_MaxPlayersSlider.value;
        input_MaxPlayers.text = maxPlayersInRoom.ToString();
    }
    
    public void GenerateRandomRoomName()
    {
        string randomRoomName = "Room_" + Random.Range(1000, 9999); // Rastgele oda ismi oluştur
        input_Create.text = randomRoomName; // Oluşturulan ismi input alanına ata
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