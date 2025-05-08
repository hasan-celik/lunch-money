using UnityEngine;

public class CarScript : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction movementDirection;  // Hareket yönü
    public float minSpeed = 5f;  // Minimum hız
    public float maxSpeed = 20f;  // Maksimum hız
    private float speed;  // Rastgele seçilecek hız

    public Sprite[] sprites;  // Sprite dizisi
    private SpriteRenderer spriteRenderer;  // SpriteRenderer bileşeni
    private Vector3 movementVector;

    void Start()
    {
        // Rastgele hız ata
        speed = Random.Range(minSpeed, maxSpeed);
        
        // SpriteRenderer bileşenini al
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Eğer SpriteRenderer bulunamadıysa hata ver
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bileşeni bulunamadı!");
            return;
        }

        // Sprite dizisinden rastgele bir sprite seç
        if (sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];
        }
        else
        {
            Debug.LogWarning("Sprite dizisi boş, sprite atanamadı!");
        }
        
        movementVector = Vector3.up;

        // Seçilen yönü belirlemek için uygun hareket vektörünü ayarla
        switch (movementDirection)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                break;
        }
        Invoke("CarSelfDestroy",10);
    }

    public void CarSelfDestroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        // GameObject'i her frame'de hareket ettir
        transform.Translate(movementVector * speed * Time.deltaTime);
    }
}