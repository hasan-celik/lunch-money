using Photon.Pun;
using UnityEngine;

public class Bell : MonoBehaviourPunCallbacks
{
    public GameObject votingCanvas;

    private void OnMouseDown()
    {
        //PhotonNetwork.LoadLevel("Voting");
        photonView.RPC("activateVoting", RpcTarget.All);
    }

    [PunRPC]
    public void activateVoting() 
    {
        votingCanvas.SetActive(true);
    }
}



