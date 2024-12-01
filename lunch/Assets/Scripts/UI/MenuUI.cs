using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainPage, _cosmeticPage, _settingsPage, _playerInfoPage, _createLobbyPage, _findGamePage;
    [SerializeField] private GameObject joinUI;
    private bool isJoinUiOpen = false;

    public void OpenMainOage()
    {
        _mainPage.SetActive(true);
        _cosmeticPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
    }
    
    public void OpenCosmeticPage()
    {
        _mainPage.SetActive(false);
        _cosmeticPage.SetActive(true);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
    }
    
    public void OpenSettingsPage()
    {
        _mainPage.SetActive(false);
        _cosmeticPage.SetActive(false);
        _settingsPage.SetActive(true);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
    }
    
    public void OpenPlayerInfoPage()
    {
        _mainPage.SetActive(false);
        _cosmeticPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(true);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(false);
    }
    
    public void OpenCreateLobbyPage()
    {
        _mainPage.SetActive(false);
        _cosmeticPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(true);
        _findGamePage.SetActive(false);
    }
    
    public void OpenFindGamePage()
    {
        _mainPage.SetActive(false);
        _cosmeticPage.SetActive(false);
        _settingsPage.SetActive(false);
        _playerInfoPage.SetActive(false);
        _createLobbyPage.SetActive(false);
        _findGamePage.SetActive(true);
    }

    public void JoinButton() 
    {
        isJoinUiOpen = !isJoinUiOpen;
    }

    private void Update()
    {
        if(isJoinUiOpen)
            joinUI.SetActive(true);
        else
            joinUI.SetActive(false);
    }
}
