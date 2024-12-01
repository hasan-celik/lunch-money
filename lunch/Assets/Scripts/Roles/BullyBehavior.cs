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
        SkillButton = GameObject.FindGameObjectWithTag("Zorba").GetComponent<Button>();
        parentObject = gameObject.transform.parent.gameObject;
        SkillButton.onClick.AddListener(() =>
        {
            kill();
        });
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("oncollisionenter calisti");
    //    if (collision.gameObject.CompareTag("Player")) 
    //    {
    //        targetPlayer = collision.gameObject;
    //    }
    //}

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    Debug.Log("oncollisiyonStay calisti");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        targetPlayer = collision.gameObject;
    //    }
    //}

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
        if (targetPlayer!= null)
            target = targetPlayer.transform.position;

        // Zorba davranışları
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Örnek bir tuş
        {
            kill();
        }
    }

    public void kill() 
    {
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

    //    if (target.x > transform.position.x && target.y > transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(target.x+1,target.y+1,transform.position.z);

    //    if (target.x > transform.position.x && target.y < transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(target.x + 1, target.y - 1,transform.position.z);

    //    if (target.x < transform.position.x && target.y > transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(target.x - 1, target.y + 1, transform.position.z);

    //    if (target.x < transform.position.x && target.y < transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(target.x - 1, target.y - 1, transform.position.z);

    //    if (target.x == transform.position.x && target.y > transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(transform.position.x, target.y + 1, transform.position.z);

    //    if (target.x == transform.position.x && target.y < transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(transform.position.x, target.y - 1, transform.position.z);

    //    if (target.x > transform.position.x && target.y == transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(target.x + 1, transform.position.y, transform.position.z);

    //    if (target.x < transform.position.x && target.y == transform.position.y)
    //        gameObject.transform.parent.gameObject.transform.position = new Vector3(target.x - 1, transform.position.y, transform.position.z);
    }
}
