using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviourPunCallbacks
{
    public GameObject taskPanel; // Görev panelini sahnede tanımla

    private bool isTaskPanelOpen = false;



    private void Start()
    {
        taskPanel.SetActive(false); // Başlangıçta panel kapalı olacak
    }


    // Görev panelini açma ve kapatma
    public void ToggleTaskPanel()
    {
        isTaskPanelOpen = !isTaskPanelOpen;
        taskPanel.SetActive(isTaskPanelOpen);
    }
}