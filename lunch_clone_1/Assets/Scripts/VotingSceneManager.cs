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
    
    public int skipCount = 0;

    private void Update()
    {
        foreach (Button b in votingPanel.GetComponentsInChildren<Button>())
        {
            if (!Buttons.Contains(b))
            {
                Buttons.Add(b);
            }
        }
        
        skipText.text = skipCount.ToString();
        countDown -= Time.deltaTime;

        _slider.value = countDown;

        if (countDown <= 0)
        {
            photonView.RPC("EndVoting", RpcTarget.All);
        }
    }

    [PunRPC]
    public void EndVoting()
    {
        GameObject maxVotedPlayer = gameManager.canli[0];
        foreach (GameObject p in gameManager.canli)
        {
            if (p.GetComponent<PlayerScript>().voteCount > maxVotedPlayer.GetComponent<PlayerScript>().voteCount)
            {
                maxVotedPlayer = p;
            }
        }

        if (maxVotedPlayer.GetComponent<PlayerScript>().voteCount > skipCount)
        {
            maxVotedPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
            maxVotedPlayer.GetComponent<PlayerScript>().onPlayerDeath();
            Debug.Log($"{maxVotedPlayer.GetComponentsInChildren<TMP_Text>()} is elliminated with {maxVotedPlayer.GetComponent<PlayerScript>().voteCount} votes");
        }
        else
        {
            Debug.Log("Skiped. No one is eliminated.");
        }

        skipCount = 0;
        Invoke("closeVoting",2);
    }

    void closeVoting()
    {
        countDown = 30;
        votingUI.SetActive(false);
    }

    public void skipInc()
    {
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
        
        photonView.RPC("skipButton", RpcTarget.All);
    }

    [PunRPC]
    public void skipButton()
    {
        skipCount++;
    }


}
