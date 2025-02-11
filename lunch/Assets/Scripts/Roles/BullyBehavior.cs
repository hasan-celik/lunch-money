using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class BullyBehavior : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private Vector3 target;
    private Button SkillButton;

    private GameObject parentObject;

    private void Start()
    {
        if (photonView.IsMine) // Sadece yerel oyuncu için
        {
            SkillButton = GameObject.FindGameObjectWithTag("Zorba").GetComponent<Button>();
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
            target = targetPlayer.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftShift)) // Sadece yerel istemci için giriş kontrolü
        {
            kill();
        }
    }

    public void kill()
    {
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
                        parentObject.transform.position = new Vector3(target.x - 1, target.y - 1, target.z);
                        break;
                    case 0:
                        parentObject.transform.position = new Vector3(target.x - 1, target.y, target.z);
                        break;
                    case 1:
                        parentObject.transform.position = new Vector3(target.x - 1, target.y + 1, target.z);
                        break;
                }
                break;
            case 0:
                switch (Mathf.Sign(target.y - transform.position.y))
                {
                    default:
                        break;
                    case -1:
                        parentObject.transform.position = new Vector3(target.x, target.y - 1, target.z);
                        break;
                    case 0:
                        parentObject.transform.position = new Vector3(target.x, target.y, target.z);
                        break;
                    case 1:
                        parentObject.transform.position = new Vector3(target.x, target.y + 1, target.z);
                        break;
                }
                break;
            case 1:
                switch (Mathf.Sign(target.y - transform.position.y))
                {
                    default:
                        break;
                    case -1:
                        parentObject.transform.position = new Vector3(target.x + 1, target.y - 1, target.z);
                        break;
                    case 0:
                        parentObject.transform.position = new Vector3(target.x + 1, target.y, target.z);
                        break;
                    case 1:
                        parentObject.transform.position = new Vector3(target.x + 1, target.y + 1, target.z);
                        break;
                }
                break;
        }
    }
}
