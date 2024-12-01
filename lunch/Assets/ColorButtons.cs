using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtons : MonoBehaviourPunCallbacks
{
    public Button lacivert, yesil, kirmizi, sari, pembe, turuncu, mor, bebekMavisi, camYesili;
    public List<GameObject> players;


    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        lacivert.onClick.AddListener(() =>
        {
            foreach (var player in players) 
            {
                if (player.GetPhotonView().IsMine)
                    player.GetComponent<Renderer>().material.color =
                    lacivert.colors.normalColor;
                //photonView.RPC(inactivateButton, RpcTarget.All, lacivert.GetComponent<Button>();
            }
        });
    }

    [PunRPC]
    public void inactivateButton(Button b) 
    {
        b.GetComponent<Button>().interactable = false;
        b.GetComponentInChildren<TMP_Text>().enabled = true;
    }
}
