using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CopySchoolBell : MonoBehaviourPunCallbacks
{
    public GameObject mainSchoolBell;

    private void Start()
    {
        mainSchoolBell = GameObject.FindGameObjectWithTag("MainSchoolBell");
        votingCanvas = mainSchoolBell.GetComponent<Bell>().votingCanvas;
        skipButton = mainSchoolBell.GetComponent<Bell>().skipButton;
        votingPanel = mainSchoolBell.GetComponent<Bell>().votingPanel;
        buttonParent = mainSchoolBell.GetComponent<Bell>().buttonParent;
        gameManager = mainSchoolBell.GetComponent<Bell>().gameManager;
    }

    public GameObject votingCanvas;
    public Button skipButton;

    private void OnMouseDown()
    {
        photonView.RPC("CreateVoteButtons", RpcTarget.All);
        photonView.RPC("activateVoting", RpcTarget.All);
        photonView.RPC("selfDestroy", RpcTarget.All);
    }

    [PunRPC]
    public void selfDestroy()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    public void activateVoting() 
    {
        foreach (var VARIABLE in gameManager.canli)
        {
            VARIABLE.GetComponent<PhotonView>().RPC("resetVote", RpcTarget.All);
        }
        Buttons.Clear();
        votingCanvas.SetActive(true);
    }

    #region votingButtons
    
    [SerializeField] private GameObject votingPanel;
    public List<Button> Buttons;

    public GameObject buttonPrefab; // Oyuncu butonları için prefab
    public Transform buttonParent; // Butonların ekleneceği panel
    public GameManager gameManager;

    private void Update()
    {
        foreach (Button b in votingPanel.GetComponentsInChildren<Button>())
        {
            if (!Buttons.Contains(b))
            {
                Buttons.Add(b);
            }
        }

        foreach (GameObject p in gameManager.canli)
        {
            foreach (Button b in votingPanel.GetComponentsInChildren<Button>())
            {
                if (p.GetComponentInChildren<TMP_Text>().text == b.GetComponentInChildren<TMP_Text>().text)
                {
                    TMP_Text[] texts = b.GetComponentsInChildren<TMP_Text>();
                    texts[1].text = p.GetComponent<PlayerScript>().voteCount.ToString();
                }
            }
        }

        foreach (var VARIABLE in gameManager.oluler)
        {
            if (VARIABLE.GetComponent<PhotonView>().IsMine)
            {
                foreach (Button b in Buttons)
                {
                    b.interactable = false;
                }
            }
        }
    }

    [PunRPC]
    void CreateVoteButtons()
    {
        // Önce mevcut butonları temizleyelim
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Tüm oyuncular için buton oluştur
        foreach (GameObject player in gameManager.canli)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            newButton.transform.SetParent(buttonParent);
            newButton.GetComponentInChildren<TMP_Text>().text = player.GetComponentInChildren<TMP_Text>().text;
            Image[] ims = newButton.GetComponentsInChildren<Image>();
            ims[1].color = player.GetComponent<Renderer>().material.color;
            newButton.GetComponent<Button>().onClick.AddListener(() => VoteForPlayer(player));
        }
        
        foreach (GameObject player in gameManager.oluler)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            newButton.transform.SetParent(buttonParent);
            newButton.GetComponentInChildren<TMP_Text>().text = player.GetComponentInChildren<TMP_Text>().text;
            Image[] ims = newButton.GetComponentsInChildren<Image>();
            ims[1].color = player.GetComponent<Renderer>().material.color;
            ims[1].rectTransform.rotation = new Quaternion(ims[1].rectTransform.rotation.x, ims[1].rectTransform.rotation.y,ims[1].rectTransform.rotation.z +90f, ims[1].rectTransform.rotation.w);
            newButton.GetComponent<Button>().interactable = false;
        }
    }

    void VoteForPlayer(GameObject player)
    {
        skipButton.interactable = false;
        
        foreach (GameObject p in gameManager.canli)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                foreach (Button b in Buttons)
                {
                    b.interactable = false;
                }
            }
        }
        
        photonView.RPC("RegisterVote", RpcTarget.All, player.GetComponentInChildren<TMP_Text>().text);
    }

    [PunRPC]
    void RegisterVote(string votedPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        
        GameObject voted = null;
        foreach (GameObject p in gameManager.canli)
        {
            if (votedPlayer == p.GetComponentInChildren<TMP_Text>().text)
            {
                voted = p;
            }
        }
        
        voted.GetComponent<PhotonView>().RPC("voting", RpcTarget.All);
    }

    #endregion
}



