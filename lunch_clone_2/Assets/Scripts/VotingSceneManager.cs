using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class VotingSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float countDown = 30;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject votingUI;
    
    private void Update()
    {
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
        countDown = 30;
        votingUI.SetActive(false);
    }
}
