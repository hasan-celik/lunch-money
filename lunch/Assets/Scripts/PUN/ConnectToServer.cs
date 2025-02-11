using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Invoke("LoadMenuScene", 2f); // 2 saniye sonra LoadMenuScene() fonksiyonunu çağır
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}