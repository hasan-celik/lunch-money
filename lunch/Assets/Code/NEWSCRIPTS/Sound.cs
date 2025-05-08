using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioSource musicSource; // Müzik için AudioSource bileşeni
    public Button toggleButton; // Buton

    public Image buttonUI;
    
    public Sprite buttonImage;
    public Sprite muteImage;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleMusic); // Butona tıklama olayını ekle
    }

    void ToggleMusic()
    {
        // Müzik açılıp kapatılıyor
        if (musicSource.isPlaying)
        {
            musicSource.Pause(); // Müzik duraklatılır
            buttonUI.sprite = muteImage;
        }
        else
        {
            musicSource.Play(); // Müzik çalmaya başlar
            buttonUI.sprite = buttonImage;
        }
    }
}
