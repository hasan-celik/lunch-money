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
    
    public Camera _mainCamera;

    private void Start()
    {
        if (photonView.IsMine)
        {
            cameraHolder = GameObject.Find("cameraHolder");
            _mainCamera = cameraHolder.transform.Find("Camera").GetComponent<Camera>();
            cameraHolder.SetActive(true);
        }
    }

    private void Update()
    {
        if ((SceneManager.GetActiveScene().name == "GamePlay" || SceneManager.GetActiveScene().name == "Lobby") && photonView.IsMine)
        {
            cameraHolder.transform.position = transform.position + offset;
        }
    }

    public void onPlDeath()
    {
        int deadLayer = LayerMask.NameToLayer("Dead");
        _mainCamera.cullingMask |= (1 << deadLayer);
    }
}
