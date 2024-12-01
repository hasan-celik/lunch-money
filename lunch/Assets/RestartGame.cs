using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        photonView.RPC("RestartTheGame", RpcTarget.All);
    }

    [PunRPC]
    public void RestartTheGame()
    {
        PhotonNetwork.LoadLevel("Menu");
    }
}
