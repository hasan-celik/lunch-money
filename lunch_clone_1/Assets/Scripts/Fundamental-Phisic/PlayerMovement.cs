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
    
    [SerializeField] private GameObject mapCircle;
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
        if (!photonView.IsMine) return;

        Vector2 move = MoveAction.ReadValue<Vector2>();

        if (move != Vector2.zero)
        {
            Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
            transform.position = position;

            // Yürüyüş sesini sadece çalmıyorsa başlat
            if (!_audioSource.isPlaying && photonView.IsMine)
                _audioSource.Play();
            
            // Flip işlemini sadece yatay harekete göre yap
            if (move.x < 0)
                _spriteRenderer.flipX = true;
            else if (move.x > 0)
                _spriteRenderer.flipX = false;
        }
        else
        {
            // Karakter durunca sesi durdur
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }

        // Animasyon Kontrolü
        _animator.SetInteger("Mode", move != Vector2.zero ? 1 : 0);
    }
    public bool GetFlipX()
    {
        return _spriteRenderer.flipX;
    }
}