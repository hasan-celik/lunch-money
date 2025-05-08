using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BullyBehavior : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private Vector3 target;
    private Button SkillButton;
    private TMP_Text nextSkillTime;

    private GameObject parentObject;
    private float nextSkillUseTime = 15f; // Cooldown için zaman
    private float skillCooldown = 30f; // 30 saniye cooldown süresi

    private void Start()
    {
        if (photonView.IsMine) // Sadece yerel oyuncu için
        {
            SkillButton = GameObject.FindGameObjectWithTag("Bully").GetComponent<Button>();
            SkillButton.interactable = false;
            nextSkillTime = SkillButton.GetComponentInChildren<TMP_Text>();
            SkillButton.onClick.AddListener(kill);
        }

        parentObject = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Yakinlik"))
        {
            targetPlayer = collision.gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Yakinlik"))
        {
            targetPlayer = null;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return; // Diğer istemcilerde giriş kontrolünü atla

        if (targetPlayer != null)
        {
            target = targetPlayer.transform.position;
            SkillButton.interactable = true;
        }
        else
        {
            SkillButton.interactable = false;
        }

        

        if (Input.GetKeyDown(KeyCode.LeftShift)) // Sadece yerel istemci için giriş kontrolü
        {
            kill();
        }

        // Cooldown süresini UI'a yazdır
        if (nextSkillTime != null)
        {
            if (Time.time < nextSkillUseTime)
            {
                nextSkillTime.text = "Skill: " + Mathf.Ceil(nextSkillUseTime - Time.time) + "s";
                SkillButton.interactable = false;
            }
            else
            {
                nextSkillTime.text = "Skill: Ready!";
                SkillButton.interactable = true;
            }
        }
    }
    
    public void kill()
    {
        if (Time.time < nextSkillUseTime) return; // Eğer cooldown bitmediyse yetenek kullanılamaz

        if (targetPlayer == null) return;

        targetPlayer.layer = 1;
        switch (Mathf.Sign(target.x - transform.position.x))
        {
            default:
                break;
            case -1:
                switch (Mathf.Sign(target.y - transform.position.y))
                {
                    default:
                        break;
                    case -1:
                    {
                        parentObject.transform.position = new Vector3(target.x - 1, target.y - 1, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                    case 0:
                    {
                        parentObject.transform.position = new Vector3(target.x - 1, target.y, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                    case 1:
                    {
                        parentObject.transform.position = new Vector3(target.x - 1, target.y + 1, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                }
                break;
            case 0:
                switch (Mathf.Sign(target.y - transform.position.y))
                {
                    default:
                        break;
                    case -1:
                    {
                        parentObject.transform.position = new Vector3(target.x, target.y - 1, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                    case 0:
                    {
                        parentObject.transform.position = new Vector3(target.x, target.y, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                    case 1:
                    {
                        parentObject.transform.position = new Vector3(target.x, target.y + 1, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                }
                break;
            case 1:
                switch (Mathf.Sign(target.y - transform.position.y))
                {
                    default:
                        break;
                    case -1:
                    {
                        parentObject.transform.position = new Vector3(target.x + 1, target.y - 1, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                    case 0:
                    {
                        parentObject.transform.position = new Vector3(target.x + 1, target.y, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                    case 1:
                    {
                        parentObject.transform.position = new Vector3(target.x + 1, target.y + 1, target.z);
                        targetPlayer.GetComponent<PhotonView>().RPC("SetRole", RpcTarget.All, PlayerRole.Dead);
                        targetPlayer.GetComponent<PlayerScript>().onPlayerDeath();
                    }
                        break;
                }
                break;
        }
        // Cooldown başlat
        nextSkillUseTime = Time.time + skillCooldown;
        
    }
}
