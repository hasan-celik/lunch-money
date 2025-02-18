using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        GameObject usernameGameObject = GameObject.FindGameObjectWithTag("playerUsername");
        string playerName = usernameGameObject.GetComponent<PlayerUsername>().playername;

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Guest_" + Random.Range(1000, 9999);
        }

        PhotonNetwork.NickName = playerName;
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