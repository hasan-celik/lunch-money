using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    public Animator characterAnimator;
    public Image buttonImage; // Butonun Image bileşeni
    public Sprite idleSprite; // Normal Sprite
    public Sprite jumpSprite; // Zıplama Sprite
    public Sprite jumpSprite2;
    
    void Start()
    {
        // Butona tıklama olayını bağla
        GetComponent<Button>().onClick.AddListener(PlayJumpAnimation);
        buttonImage.sprite = idleSprite; // İlk başta normal sprite göster
        characterAnimator.SetBool("JumpTrigger",false);
    }

    void PlayJumpAnimation()
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("JumpTrigger",true); // Animasyonu tetikle
            buttonImage.sprite = jumpSprite; // Buton görüntüsünü değiştir
            Invoke("Jump2", 0.25f);
            Invoke("ResetSprite", 0.5f); // Belirli bir süre sonra sprite'ı eski haline getir
        }
    }

    void Jump2()
    {
        buttonImage.sprite = jumpSprite2;
    }

    void ResetSprite()
    {
        buttonImage.sprite = idleSprite; // Tekrar eski sprite'a döndür
        characterAnimator.SetBool("JumpTrigger",false);
    }
}