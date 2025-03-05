using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    // Bu objeye çarpan Car objelerini yok etmek için
    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer çarpan objenin tag'ı "Car" ise
        if (other.CompareTag("car"))
        {
            Destroy(other.gameObject);  // Objeyi yok et
        }
    }
}