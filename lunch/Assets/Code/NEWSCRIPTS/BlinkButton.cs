using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkButton : MonoBehaviour
{
    public Image buttonImage; // Butonun Image bileşeni
    public Sprite[] blinkSprites; // Zıplama Sprites Dizisi
    public float frameRate = 0.1f; // Her sprite karesi kaç saniyede değişecek?

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => StartCoroutine(PlayBlinkAnimation()));
    }

    IEnumerator PlayBlinkAnimation()
    {
        for (int i = 0; i < blinkSprites.Length; i++)
        {
            buttonImage.sprite = blinkSprites[i];
            yield return new WaitForSeconds(frameRate); // Belirtilen süre kadar bekle
        }
    }
}