using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingRun : MonoBehaviour
{
    public Image buttonImage; // Butonun Image bileşeni
    public Sprite[] jumpSprites; // Zıplama Sprites Dizisi
    public float frameRate = 0.1f; // Her sprite karesi kaç saniyede değişecek?

    void Start()
    {
        if (jumpSprites.Length > 0)
        {
            StartCoroutine(AnimateSprites());
        }
    }

    IEnumerator AnimateSprites()
    {
        while (true) // Sonsuz döngü ile sürekli animasyon oynat
        {
            for (int i = 0; i < jumpSprites.Length; i++)
            {
                buttonImage.sprite = jumpSprites[i]; // Sprite'ı değiştir
                yield return new WaitForSeconds(frameRate); // Belirtilen süre bekle
            }
        }
    }
}