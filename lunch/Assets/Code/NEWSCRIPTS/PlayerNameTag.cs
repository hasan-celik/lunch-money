using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameTag : MonoBehaviour
{
    public TMP_Text nameText;

    private void Start()
    {
        if (TryGetComponent(out PhotonView photonView))
        {
            if (photonView.IsMine)
            {
                photonView.RPC("SetPlayerName", RpcTarget.AllBuffered, PhotonNetwork.NickName);
            }
        }
    }

    [PunRPC]
    public void SetPlayerName(string playerName)
    {
        if (nameText != null)
        {
            nameText.text = playerName;
        }
    }
}