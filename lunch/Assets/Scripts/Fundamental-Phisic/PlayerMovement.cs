using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private InputAction MoveAction;
    public GameObject mark;

    [SerializeField] private Animator _animator;
    
    private void Start()
    {
        MoveAction.Enable();
        _animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if (GetComponent<PhotonView>().IsMine == true)
        {
            Vector2 move = MoveAction.ReadValue<Vector2>();
            //Debug.Log(move);
            Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
            transform.position = position;
        }

        if (GetComponent<PhotonView>().IsMine && MoveAction.IsInProgress())
        {
            _animator.SetInteger("Mode", 1);
        }
        else
        {
            _animator.SetInteger("Mode", 0);
        }
    }
}