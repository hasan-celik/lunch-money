using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RestartSpawner : MonoBehaviour
{
    private GameObject restartGame;
    // Start is called before the first frame update
    void Start()
    {
        restartGame = PhotonNetwork.Instantiate("RestartGame", Vector3.one, Quaternion.identity);
        restartGame.GetComponent<RestartGame>().gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (restartGame.GetComponent<RestartGame>().gameManager == null)
        {
            restartGame.GetComponent<RestartGame>().gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }
}
