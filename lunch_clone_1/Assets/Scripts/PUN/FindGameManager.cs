using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class FindGameManager : MonoBehaviourPunCallbacks
{
    public GameObject roomItemPrefab; // Oda prefabı
    public Transform roomListParent; // Oda prefablarının yerleşeceği alan

    private List<GameObject> roomItemList = new List<GameObject>();

    public void OpenFindGameCanvas()
    {
        PhotonNetwork.JoinLobby(); // Lobide değilsek, lobiye giriyoruz
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Önceki odaları temizle
        foreach (GameObject item in roomItemList)
        {
            Destroy(item);
        }
        roomItemList.Clear();

        // Yeni odaları ekle
        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList) // Kapalı veya dolu odaları listeleme
            {
                GameObject newRoomItem = Instantiate(roomItemPrefab, roomListParent);
                newRoomItem.GetComponent<RoomItem>().SetRoomInfo(room.Name.ToString(), room.IsOpen.ToString(),
                    room.PlayerCount.ToString(), room.MaxPlayers.ToString(),
                    FindObjectOfType<CreateAndJoin>());
                roomItemList.Add(newRoomItem);
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye girildi, oda listesi güncellenecek.");
    }
}