using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VotingSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float countDown = 30;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject votingUI;
    [SerializeField] private GameObject votingPanel;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text skipText;
    public List<Button> Buttons;
    public Button votingSkipButton;
    
    private bool votingEnded = false;

    
    public int skipCount = 0;
    private HashSet<int> playersVoted = new HashSet<int>();  // Players who have voted.
    
    private void Start()
    {
        foreach (Button b in votingPanel.GetComponentsInChildren<Button>())
        {
            if (!Buttons.Contains(b))
            {
                Buttons.Add(b);
            }
        }
    }

    private void Update()
    {
        if (votingEnded) return; // Eğer oylama bittiyse işlem yapma

        skipText.text = skipCount.ToString();
        countDown -= Time.deltaTime;

        _slider.value = countDown;

        if (PhotonNetwork.IsMasterClient && countDown <= 0 && votingEnded == false)
        {
            photonView.RPC("EndVoting", RpcTarget.All);
        }
    }


    [PunRPC]
    public void EndVoting()
    {
        if (votingEnded) return; // Eğer zaten çalıştıysa tekrar çalıştırma.
        
        playersVoted.Clear(); // Oy verenler listesi temizleniyor.

        int maxVotes = 0;
        GameObject maxVotedPlayer = null;

        // En çok oyu alan oyuncuyu bul
        foreach (GameObject p in gameManager.canli)
        {
            int playerVotes = p.GetComponent<PlayerScript>().voteCount;

            if (playerVotes > maxVotes)
            {
                maxVotes = playerVotes;
                maxVotedPlayer = p;
            }
            else if (playerVotes == maxVotes) // Eşit oy varsa kimse elenmez
            {
                maxVotedPlayer = null;
            }
        }

        // Eğer skipCount çoğunluktaysa veya eşit oy varsa kimse elenmemeli
        if (skipCount >= maxVotes || maxVotedPlayer == null)
        {
            Debug.Log("Voting skipped. No one is eliminated.");
        }
        else
        {
            // En çok oyu alan oyuncuyu ele
            maxVotedPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
            maxVotedPlayer.GetComponent<PlayerScript>().onPlayerDeath();
            Debug.Log($"{maxVotedPlayer.GetComponent<PhotonView>().Owner.NickName} is eliminated with {maxVotes} votes");
        }

        skipCount = 0;
        playersVoted.Clear(); // Oy verenleri sıfırla
        Invoke("closeVoting", 2);
        
        votingEnded = true; // Voting tamamlandı.
    }


    void closeVoting()
    {
        countDown = 30;
        votingUI.SetActive(false);
        votingEnded = false; // Yeni oylama için sıfırla
    }


    public void skipInc()
    {
        foreach (GameObject p in gameManager.canli)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                if (playersVoted.Contains(p.GetComponent<PhotonView>().ViewID)) return; // Oy vermişse işlem yapma

                foreach (Button b in Buttons)
                {
                    b.interactable = false;
                }

                photonView.RPC("skipButton", RpcTarget.All);
                votingSkipButton.interactable = false;
                playersVoted.Add(p.GetComponent<PhotonView>().ViewID); // Oy veren olarak işaretle
            }
        }
    }


    [PunRPC]
    public void skipButton()
    {
        int myViewID = PhotonNetwork.LocalPlayer.ActorNumber;
        if (playersVoted.Contains(myViewID)) return; // Oy vermişse tekrar sayma
        playersVoted.Add(myViewID); // Oy veren olarak işaretle
        skipCount++;
    }


}
