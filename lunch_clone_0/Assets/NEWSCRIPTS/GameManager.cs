using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    private Slider _slider;
    public List<GameObject> players;
    public List<GameObject> zorbalar;
    public List<GameObject> Ogrenciler;
    public List<GameObject> oluler;
    public List<GameObject> canli;
    
    public GameObject winCanvas;
    public GameObject loseCanvas;
    
    public int bullyIndex;
    private int buPlayerinIndexi;
    

    private void Start()
    {
        _slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        buPlayerinIndexi = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (PhotonNetwork.IsMasterClient)
        {
            int randomIndex = Random.Range(0, players.Count);
            photonView.RPC("SetBullyIndex", RpcTarget.AllBuffered, randomIndex);
        }
        AssignRoles(bullyIndex);
    }
    
    [PunRPC]
    void SetBullyIndex(int index)
    {
        bullyIndex = index;
    }
    
    private void Update()
    {
        foreach (GameObject p in players)
        {
            if (p == null)
            {
                players.Remove(p);
                zorbalar.Remove(p);
                Ogrenciler.Remove(p);
                oluler.Remove(p);
                canli.Remove(p);
            }
            
            if (p.GetComponent<PlayerScript>().role == PlayerRole.Student)
            {
                if (!Ogrenciler.Contains(p))
                {
                    Ogrenciler.Add(p);
                }

                if (zorbalar.Contains(p))
                {
                    zorbalar.Remove(p);
                }

                if (oluler.Contains(p))
                {
                    oluler.Remove(p);
                }
            }
            else if (p.GetComponent<PlayerScript>().role == PlayerRole.Bully)
            {
                if (p == null)
                {
                    players.Remove(p);
                    zorbalar.Remove(p);
                    Ogrenciler.Remove(p);
                    oluler.Remove(p);
                    canli.Remove(p);
                }
                
                if (!zorbalar.Contains(p))
                {
                    zorbalar.Add(p);
                }
                
                if (Ogrenciler.Contains(p))
                {
                    Ogrenciler.Remove(p);
                }
                
                if (oluler.Contains(p))
                {
                    oluler.Remove(p);
                }
            }
            else if (p.GetComponent<PlayerScript>().role == PlayerRole.Dead)
            {
                if (p == null)
                {
                    players.Remove(p);
                    zorbalar.Remove(p);
                    Ogrenciler.Remove(p);
                    oluler.Remove(p);
                    canli.Remove(p);
                }

                if (zorbalar.Contains(p))
                {
                    zorbalar.Remove(p);
                }
                
                if (Ogrenciler.Contains(p))
                {
                    Ogrenciler.Remove(p);
                }
                
                if (canli.Contains(p))
                {
                    canli.Remove(p);
                }

                if (!oluler.Contains(p))
                {
                    oluler.Add(p);
                }
            }
            
            if(p.GetComponent<PlayerScript>().role == PlayerRole.Student || p.GetComponent<PlayerScript>().role == PlayerRole.Bully)
            {
                if (oluler.Contains(p))
                {
                    oluler.Remove(p);
                }
                
                if (!canli.Contains(p))
                {
                    canli.Add(p);
                }
            }
        }
        
        if (players.Count()== zorbalar.Count()+Ogrenciler.Count()+oluler.Count())
        {
            if (_slider != null)
            {
                // Win condition: slider reached max value or no zorbalar left
                if (_slider.value == _slider.maxValue || zorbalar.Count == 0)
                {
                    photonView.RPC("WinGame", RpcTarget.All);
                }
            }

            // Lose condition: If the number of zorbalar is greater than or equal to students
            if (zorbalar.Count() >= Ogrenciler.Count())
                photonView.RPC("LoseGame", RpcTarget.All);
        }
    }

    [PunRPC]
    public void WinGame()
    {
        winCanvas.SetActive(true);
    }

    [PunRPC]
    public void LoseGame()
    {
        loseCanvas.SetActive(true);
    }
    
    void AssignRoles(int x)
    {
        foreach (var player in players)
        {
            if (x == player.GetComponent<PhotonView>().OwnerActorNr-1)
            {
                player.GetComponent<PlayerScript>().ChangeRole(PlayerRole.Bully);
            }
            else
            {
                player.GetComponent<PlayerScript>().ChangeRole(PlayerRole.Student);
            }
        }
    }
}