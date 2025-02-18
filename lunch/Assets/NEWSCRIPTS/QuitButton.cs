using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    public Button quitButton; // Çıkış butonu

    void Start()
    {
        quitButton.onClick.AddListener(QuitGame); // Butona tıklama olayını ekle
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Oyun editörde çalışıyorsa durdur
#else
            Application.Quit(); // Oyun build alınmışsa tamamen kapat
#endif
    }
}