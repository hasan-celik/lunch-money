using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d : MonoBehaviour
{
    public void listele()
    {
        // Oyun başladığında Ready durumunu kontrol et
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready"))
        {
            bool isReady = (bool)PhotonNetwork.LocalPlayer.CustomProperties["Ready"];
            Debug.Log("Ready Durumu: " + isReady);
            
            
        }
        else
        {
            Debug.Log("Ready özelliği bulunmuyor.");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master client " );
        }
        else
        {
            Debug.Log("client ");
        }
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
