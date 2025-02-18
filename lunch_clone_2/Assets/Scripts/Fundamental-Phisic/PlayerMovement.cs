using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private InputAction MoveAction;
    public GameObject mark;

    [SerializeField] private Animator _animator;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource _audioSource;
    
    [SerializeField] private GameObject  mapCircle;
    [SerializeField] private GameObject light;

    private void Start()
    {
        MoveAction.Enable();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        if (photonView.IsMine)
        {
            mapCircle.SetActive(true);
            light.SetActive(true);
        }
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
            _audioSource.Play();
        }

        if (GetComponent<PhotonView>().IsMine && Keyboard.current.aKey.IsPressed())
        {
            _spriteRenderer.flipX = true;
        }

        if (GetComponent<PhotonView>().IsMine && Keyboard.current.dKey.IsPressed())
        {
            _spriteRenderer.flipX = false;
        }
    }
}