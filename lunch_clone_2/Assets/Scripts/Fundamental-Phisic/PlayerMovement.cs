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
    
    private int _lastMode = -1;
    
    void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        
        Vector2 move = MoveAction.ReadValue<Vector2>();

        if (move != Vector2.zero)
        {
            Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime;
            transform.position = position;

            if (!_audioSource.isPlaying)
                _audioSource.Play();

            if (move.x < 0)
                _spriteRenderer.flipX = true;
            else if (move.x > 0)
                _spriteRenderer.flipX = false;
        }
        else
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }

        // Mode değiştiyse sadece o zaman güncelle
        int newMode = move != Vector2.zero ? 1 : 0;
        if (_lastMode != newMode)
        {
            _animator.SetInteger("Mode", newMode);
            _lastMode = newMode;
        }
    }
    
    private float blinkTimer = 0f;

    void Update()
    {
        if (_animator.GetInteger("Mode") == 0) // Sadece Idle durumunda
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0)
            {
                _animator.SetTrigger("Blink");
                blinkTimer = Random.Range(6f, 10f); // 3-6 saniye arasında rastgele tekrar et
            }
        }
    }

    public bool GetFlipX()
    {
        return _spriteRenderer.flipX;
    }
}