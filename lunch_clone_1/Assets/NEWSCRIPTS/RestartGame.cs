using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class RestartGame : MonoBehaviourPunCallbacks
{
    public GameManager gameManager;

    // private void Awake()
    // {
    //     gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    // }

    private void Start()
    {
        photonView.RPC("RestartTheGame", RpcTarget.All);
    }

    [PunRPC]
    public void RestartTheGame()
    {
        // Sadece kendi oyuncumu bul ve yeniden oluştur
        foreach (var p in gameManager.players)
        {
            PhotonView pv = p.GetComponent<PhotonView>();

            if (pv != null && pv.IsMine) // Eğer p benim karakterimse
            {
                Vector3 spawnPos = new Vector3(Random.Range(-15, 26), Random.Range(-6, 17), p.transform.position.z);

                // Yeni oyuncuyu oluştur
                GameObject newPlayer = PhotonNetwork.Instantiate("PlayerPrefab", spawnPos, Quaternion.identity);

                // Eski oyuncuyu sil
                PhotonNetwork.Destroy(p);
            }
        }
    }
}
