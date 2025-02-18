using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListManager : MonoBehaviourPunCallbacks
{
    public static PlayerListManager Instance;
    private List<string> playerNames = new List<string>(); // Oyuncu isimlerini saklayan liste

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişince yok olmasın
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        photonView.RPC("SyncPlayerList", RpcTarget.AllBuffered, PhotonNetwork.NickName);
    }

    [PunRPC]
    private void SyncPlayerList(string newPlayer)
    {
        if (!playerNames.Contains(newPlayer)) // Tekrar eklememek için kontrol
        {
            playerNames.Add(newPlayer);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        photonView.RPC("RemovePlayerFromList", RpcTarget.AllBuffered, otherPlayer.NickName);
    }

    [PunRPC]
    private void RemovePlayerFromList(string playerName)
    {
        if (playerNames.Contains(playerName))
        {
            playerNames.Remove(playerName);
        }
    }

    public List<string> GetPlayerNames()
    {
        return new List<string>(playerNames); // Listeyi dışarıdan değiştirilmemesi için kopyasını döndürüyoruz
    }
}