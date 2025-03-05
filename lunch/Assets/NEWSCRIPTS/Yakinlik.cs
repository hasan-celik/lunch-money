using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Yakinlik :  MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject gorev;
    public Button InteractButton;
    public TaskManager t;
    
    public Bell bell;
    public CopyMainSchoolBell copyBell;

    private void Start()
    {
        InteractButton = GameObject.FindGameObjectWithTag("InteractButton").GetComponent<Button>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("task") && photonView.IsMine)
        {
            gorev = collision.gameObject;
            t = gorev.GetComponent<TaskManager>();
            gorev.GetComponent<TaskTrigger>().enabled = true;
            
            InteractButton.onClick.AddListener(() =>
            {
                t.ToggleTaskPanel();
            });
            InteractButton.interactable = true;
        }

        if (collision.gameObject.CompareTag("MainSchoolBell") && photonView.IsMine)
        {
            bell = collision.gameObject.GetComponent<Bell>();
            
            InteractButton.onClick.AddListener(() =>
            {
                bell.startVotingMethodForOtherInputDevices();
            });
            
            InteractButton.interactable = true;
        }
        
        if (collision.gameObject.CompareTag("CopySchoolBell") && photonView.IsMine)
        {
            copyBell = collision.gameObject.GetComponent<CopyMainSchoolBell>();
            
            InteractButton.onClick.AddListener(() =>
            {
                bell.startVotingMethodForOtherInputDevices();
            });
            
            InteractButton.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("task") && photonView.IsMine)
        {
            gorev.GetComponent<TaskTrigger>().enabled = false;
            gorev = null;
            InteractButton.onClick.RemoveAllListeners();
            InteractButton.interactable = false;
        }

        if (collision.gameObject.CompareTag("MainSchoolBell") && photonView.IsMine)
        {
            gorev = null;
            bell = null;
            InteractButton.onClick.RemoveAllListeners();
            InteractButton.interactable = false;
        }

        if (collision.gameObject.CompareTag("CopySchoolBell") && photonView.IsMine)
        {
            copyBell = null;
            InteractButton.onClick.RemoveAllListeners();
            InteractButton.interactable = false;
        }
    }
    
    private void Update()
    {
        // Eğer E tuşuna basıldıysa ve buton etkileşimliyse onclick event'ini tetikle
        if (Input.GetKeyDown(KeyCode.E) && InteractButton != null && InteractButton.interactable)
        {
            InteractButton.onClick.Invoke();
        }
    }
}
