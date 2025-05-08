using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class Renk : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    
    [SerializeField] private Button kirmizi;
    [SerializeField] private Button mavi;
    [SerializeField] private Button yesil;
    
    // Start is called before the first frame update
    void Start()
    {
        kirmizi.onClick.AddListener(() =>
        {
                SpriteRenderer playerRenderer = playerPrefab.GetComponent<SpriteRenderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.material.color = Color.red;
                }
        });
        
        mavi.onClick.AddListener(() =>
        {
                SpriteRenderer playerRenderer = playerPrefab.GetComponent<SpriteRenderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.material.color = (Color)PhotonNetwork.LocalPlayer.CustomProperties["playerColor"];
                }
        });
        
        yesil.onClick.AddListener(() =>
        {
            
            if (photonView.IsMine)
            {
                SpriteRenderer playerRenderer = playerPrefab.GetComponent<SpriteRenderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.material.color = (Color)PhotonNetwork.LocalPlayer.CustomProperties["playerColor"];
                }
            }
        });
    }
}
