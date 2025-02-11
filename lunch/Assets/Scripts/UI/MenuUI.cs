using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainPage, _settingsPage, _playerInfoPage, _createLobbyPage, _findGamePage;
    
    [SerializeField] private GameObject joinUI;
    private bool isJoinUiOpen = false;
    
    [SerializeField] private GameObject SoundUI;
    [SerializeField] private GameObject ExitUI;
    private bool isSettingsUiOpen = false;
    
    public AudioSource audioSource; // Ses oynatıcı
    public AudioClip buttonClickSound; // Buton sesi

    public void OpenMainOage()
    {
        _mainPage.SetActive(true);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
        PlaySound();
    }
    
    public void OpenCosmeticPage()
    {
        _mainPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
    }
    
    public void OpenSettingsPage()
    {
        _mainPage.SetActive(false);
        _settingsPage.SetActive(true);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
        PlaySound();
    }
    
    public void OpenPlayerInfoPage()
    {
        _mainPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(true);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
        PlaySound();
    }
    
    public void OpenCreateLobbyPage()
    {
        _mainPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(true);
        _findGamePage.SetActive(false);
        PlaySound();
    }
    
    public void OpenFindGamePage()
    {
        _mainPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(true);
        PlaySound();
    }

    public void JoinButton() 
    {
        isJoinUiOpen = !isJoinUiOpen;
        PlaySound();
    }
    
    public void SettingsButton() 
    {
        isSettingsUiOpen = !isSettingsUiOpen;
    }

    private void Update()
    {
        if(isJoinUiOpen)
            joinUI.SetActive(true);
        else
            joinUI.SetActive(false);

        if (isSettingsUiOpen)
        {
            SoundUI.SetActive(true);
            ExitUI.SetActive(true);
        }
        else
        {
            SoundUI.SetActive(false);
            ExitUI.SetActive(false);
        }
    }

    void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound); // Ses efektini çal
        }
    }
}
