using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        if (photonView.IsMine)
        {
            cameraHolder = GameObject.Find("cameraHolder");
            cameraHolder.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if ((SceneManager.GetActiveScene().name == "GamePlay" || SceneManager.GetActiveScene().name == "Lobby") && photonView.IsMine)
        {
            cameraHolder.transform.position = transform.position + offset;
        }
    }
}
