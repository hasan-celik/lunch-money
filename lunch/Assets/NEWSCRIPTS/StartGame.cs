using System;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StartGame : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text startButtonText;
    [SerializeField] private GameObject gm;

    public List<GameObject> players;

    [SerializeField] private GameObject miniMap;
    [SerializeField] private GameObject roleUI;
    [SerializeField] private GameObject taskSlider;
    [SerializeField] private GameObject skillButton;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject renkButton;
    [SerializeField] private GameObject infoUI;
    [SerializeField] private GameObject cosmeticsUI;

    private Transform parentTransform;

    private void Awake()
    {
        startButton = GameObject.Find("StartGameButton").GetComponent<Button>();
    }

    private void Start()
    {
        parentTransform = GameObject.Find("Canvas").transform;
        
        Transform setups = GameObject.Find("SET-UPS").transform;
        
        
        startButtonText = startButton.GetComponentInChildren<TMP_Text>();
        gm = setups.Find("GameManager").GameObject();
        
        miniMap = parentTransform.Find("miniMap (1)").GameObject();
        roleUI = parentTransform.Find("RoleUI").GameObject();
        //taskSlider = parentTransform.Find("Slider").GameObject();
        skillButton = parentTransform.Find("SkillButton").GameObject();
        interactButton = parentTransform.Find("InteractButton").GameObject();
        renkButton = parentTransform.Find("ChangeColor").GameObject();
        infoUI = parentTransform.Find("info").GameObject();
        
        cosmeticsUI = parentTransform.Find("CosmeticPanel").GameObject();
        
        
        // Eğer "Ready" özelliği yoksa, false olarak ayarla
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", false } });
        }
        
        // MasterClient için "Start Game", diğerleri için "Ready"
        UpdateButtonState();

        startButton.onClick.AddListener(() =>
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (AllPlayersReady())
                {
                    if (players.Count >= 5)
                    {
                        StartGameNow();
                    }
                    else
                    {
                        Debug.Log("En az 5 oyuncu olması lazım");
                    }
                }
                else
                {
                    Debug.Log("All players must be ready!");
                }
            }
            else
            {
                ToggleReadyState();
            }
        });
    }

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
    }

    void UpdateButtonState()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", true } });
            startButtonText.text = "Start Game";
            startButton.image.color = Color.blue;
        }
        else
        {
            startButtonText.text = "Ready";
            UpdateReadyButtonColor();
        }
    }

    void ToggleReadyState()
    {
        // Ready özelliği kontrol edilmeden önce "Ready" key'inin varlığını kontrol et.
        bool isReady = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready") 
                       && (bool)PhotonNetwork.LocalPlayer.CustomProperties["Ready"];

        // "Ready" key'i yoksa, false olarak ayarla.
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", false } });
            isReady = false; // Ready key'i yoksa varsayılan olarak false.
        }

        // "Ready" key'inin değerini değiştir.
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", !isReady } });

        // Butonun rengini ve metnini güncelle.
        UpdateReadyButtonColor();
        photonView.RPC("UpdateAllPlayersReadyState", RpcTarget.All);
    }

    void UpdateReadyButtonColor()
    {
        // Ready key'ini kontrol et ve varsayılan olarak false ayarla.
        bool isReady = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready") 
                       && (bool)PhotonNetwork.LocalPlayer.CustomProperties["Ready"];

        // Buton rengini ve metnini güncelle.
        startButton.image.color = isReady ? Color.green : Color.red;
        startButtonText.text = isReady ? "Ready (Click to Unready)" : "Ready";
    }


    bool AllPlayersReady()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (!(bool)p.CustomProperties["Ready"])
            {
                return false;
            }
        }
        return true;
    }

    void StartGameNow()
    {
        photonView.RPC("startGameForAllPlayers", RpcTarget.All);
    }

    [PunRPC]
    public void startGameForAllPlayers()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player"); // Bütün oyuncuları al

        foreach (GameObject player in allPlayers)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                miniMap.SetActive(true);
                roleUI.SetActive(true);
                //taskSlider.SetActive(true);
                skillButton.SetActive(true);
                interactButton.SetActive(true);
                gm.SetActive(true);
                renkButton.SetActive(false);
                startButton.gameObject.SetActive(false);                
                player.GetComponent<PlayerScript>().enabled = true;
                
                cosmeticsUI.SetActive(false);
                
                infoUI.SetActive(false);
                
                player.GetComponentInChildren<Yakinlik>().enabled = true;
                player.transform.position = new Vector3(Random.Range(-16, 25), Random.Range(-17, -7), player.transform.position.z);
            }
            
        }
    }
    
    [PunRPC]
    void UpdateAllPlayersReadyState()
    {
        // Eğer master client değilse, bu fonksiyonu çalıştırma
        if (PhotonNetwork.IsMasterClient)
        {
            // Tüm oyuncuların hazır olup olmadığını kontrol et
            if (AllPlayersReady())
            {
                startButton.image.color = Color.green; // Eğer herkes hazırsa yeşil renk
            }
            else
            {
                startButton.image.color = Color.blue;
            }
        }
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("Ready"))
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                UpdateReadyButtonColor();
            }
            photonView.RPC("UpdateAllPlayersReadyState", RpcTarget.All);
        }
    }
}