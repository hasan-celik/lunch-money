using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TaskTrigger : MonoBehaviourPunCallbacks
{
    private TaskManager taskManager;

    private void Start()
    {
        taskManager = GetComponent<TaskManager>();
    }


    private void OnMouseDown()
    {
        if (taskManager != null)
        {
            taskManager.ToggleTaskPanel();
        }
    }
}
