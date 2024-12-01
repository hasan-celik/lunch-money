using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Slider _slider;
    public List<GameObject> players;
    public List<GameObject> zorbalar;
    public List<GameObject> Ogrenciler;


    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        foreach (GameObject p in players)
        {
            if (p.GetComponent<PlayerScript>().role == PlayerRole.Ogrenci)
                Ogrenciler.Add(p);
            else
                zorbalar.Add(p);
        }
    }

    private void Update()
    {
        if (_slider.value == _slider.maxValue) 
        {
            photonView.RPC("WinGame", RpcTarget.All);
        }

        //players = GameObject.FindGameObjectsWithTag("Player").ToList();

        //foreach (GameObject p in players) 
        //{
        //    if (p.GetComponent<PlayerScript>().role == PlayerRole.Ogrenci)
        //        Ogrenciler.Add(p);
        //    else
        //        zorbalar.Add(p);
        //}

        if (zorbalar.Count() >= Ogrenciler.Count())
            photonView.RPC("LoseGame", RpcTarget.All);

    }

    [PunRPC]
    public void WinGame()
    {
        PhotonNetwork.LoadLevel("WinScene");
    }

    [PunRPC]
    public void LoseGame()
    {
        PhotonNetwork.LoadLevel("LoseScene");
    }
}
