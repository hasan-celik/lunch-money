using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticButtons : MonoBehaviour
{
    [SerializeField] private Button HatButton;
    [SerializeField] private Button colorButton;
    
    [SerializeField] private GameObject hatPanel;
    [SerializeField] private GameObject colorPanel;
    
    public void OpenColorPanel()
    {
        colorPanel.SetActive(true);
        hatPanel.SetActive(false);
    }
    
    public void OpenHatPanel()
    {
        colorPanel.SetActive(false);
        hatPanel.SetActive(true);
    }
}
