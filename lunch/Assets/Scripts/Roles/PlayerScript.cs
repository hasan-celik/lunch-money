using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    private GameObject deadPlayerBell;
    
    public PlayerRole role;
    [SerializeField] private BullyBehavior bullyBehavior;
    [SerializeField] private TMP_Text PlayerRoleText;
    private Image roleImage;

    [SerializeField] private Sprite bullyImage;
    [SerializeField] private Sprite studentImage;

    public List<GameObject> zorbaGameObjects;

    public GameObject _hatHolder;
    public GameObject _yakinlik;

    private int bully1;
    private int bully2;
    
    public int voteCount = 0;
    
    void Start()
    {
        PlayerRoleText = GameObject.FindGameObjectWithTag("RoleUI").GetComponent<TMP_Text>();
        roleImage = GameObject.FindGameObjectWithTag("RoleImage").GetComponent<Image>();
        zorbaGameObjects = GameObject.FindGameObjectsWithTag("Bully").ToList();
    }

    private void Update()
    {
        if (role == PlayerRole.Bully)
        {
            PlayerRoleText.text = "Bully";

            if (photonView.IsMine)
            {
                roleImage.sprite = bullyImage;
            }

            bullyBehavior.enabled = true;
            foreach (GameObject obj in zorbaGameObjects)
            {
                obj.SetActive(true);
            }
        }
        else if(role == PlayerRole.Student)
        {
            PlayerRoleText.text = "Student";
            
            if (photonView.IsMine)
            {
                roleImage.sprite = studentImage;
            }
            
            bullyBehavior.enabled = false;
            foreach (GameObject obj in zorbaGameObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            bullyBehavior.enabled = false;
            foreach (GameObject obj in zorbaGameObjects)
            {
                obj.SetActive(false);
            }
        }

        if (gameObject.layer == 7)
        {
            photonView.RPC("syncHatHolderLayer", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void syncHatHolderLayer()
    {
        SetLayerRecursively(_hatHolder.gameObject, 7);
        _yakinlik.layer = 7;
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    [PunRPC]
    public void SetRole(PlayerRole newRole)
    {
        role = newRole;
    }

    public void ChangeRole(PlayerRole newRole)
    {
        photonView.RPC("SetRole", RpcTarget.All, newRole);
    }
    
    [PunRPC]
    public void Death()
    {
        gameObject.transform.position = new Vector3(Random.Range(-68, -58), 1.5f, 0);
        int deadLayer = LayerMask.NameToLayer("Dead");
        gameObject.layer = deadLayer;
        if (photonView.IsMine)
        {
            gameObject.GetComponent<CameraController>().onPlDeath();
        }
    }

    public void onPlayerDeath()
    {
        if (deadPlayerBell == null)
        {
            deadPlayerBell = PhotonNetwork.Instantiate("deadPlayerBell",gameObject.transform.position,gameObject.transform.rotation);
        }
        photonView.RPC("Death", RpcTarget.All);
    }
    
    [PunRPC]
    public void PlayerColor(float r, float g, float b, float a)
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(r, g, b, a);
    }

    [PunRPC]
    public void setColor(float r, float g, float b, float a)
    {
        photonView.RPC("PlayerColor", RpcTarget.All, r, g, b, a);
    }
    
    [PunRPC]
    public void vote()
    {
        voteCount++;
    }

    [PunRPC]
    public void voting()
    {
        vote();
    }


    
    [PunRPC]
    public void resetVote()
    {
        voteCount = 0;
    }
}