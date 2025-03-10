using UnityEngine;

public class RandomStart : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            // Rastgele bir başlangıç süresi belirle (0 ile 2 saniye arasında)
            float randomDelay = Random.Range(0f, 2f);

            // Animasyonu belirlenen süre sonra başlat
            StartCoroutine(StartAnimationWithDelay(randomDelay));
        }
        else
        {
            Debug.LogError("Animator bileşeni bulunamadı!");
        }
    }

    private System.Collections.IEnumerator StartAnimationWithDelay(float delay)
    {
        // Belirtilen süre kadar bekle
        yield return new WaitForSeconds(delay);

        // Animasyonu rastgele zamanla başlat
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, Random.value);
    }
}