using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Linq;

public class Spawner : MonoBehaviourPunCallbacks
{
    public List<GameObject> players;

    public Color[] colors = new Color[] {Color.blue, Color.grey, Color.magenta, Color.cyan, Color.green,Color.red,
    Color.yellow,Color.white};
    private HashSet<Color> usedColors = new HashSet<Color>();

    [SerializeField] private GameObject colorPicker;
    [SerializeField] private Button ColorPickerButton;
    public List<GameObject> ColorButtons;
    private bool isColorPanelOpen = false;
    
    [SerializeField] private GameObject canvas;

    private GameObject _slider;

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        ColorPickerButton.onClick.AddListener(() =>
        {
            ColorPanel();
        });
        ColorButtons = GameObject.FindGameObjectsWithTag("ColorButton").ToList();
        SpawnObject();
    }
    
    public void SpawnObject()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Instantiate("StartGame", Vector3.one, Quaternion.identity);
                PhotonNetwork.Instantiate("Slider", new Vector3(20,1060,0), Quaternion.identity);
            }
            
            GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-68, -58), 1.5f, 0), quaternion.identity);
        }
    }
    
    private Color GetRandomColor()
    {
        List<Color> availableColors = new List<Color>();

        foreach (var color in colors)
        {
            if (!usedColors.Contains(color))
            {
                availableColors.Add(color);
            }
        }

        if (availableColors.Count > 0)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            return availableColors[randomIndex];
        }

        return Color.clear; 
    }

    public void ColorSet(GameObject p, Color color)
    {
        p.GetComponent<Renderer>().material.color = color;
    }

    public void ColorPanel() 
    {
        isColorPanelOpen = !isColorPanelOpen;
        if (isColorPanelOpen) 
        {
            colorPicker.SetActive(true);
        }
        else
            colorPicker.SetActive(false);
    }

    private void Update()
    {
        baslangic:
        if (_slider == null && canvas != null)
        {
            try
            {
                _slider = GameObject.FindGameObjectWithTag("Slider");
            }
            catch (Exception e)
            {
                if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
                {
                    if (photonView.IsMine)
                    {
                        PhotonNetwork.Instantiate("Slider", new Vector3(20, 1060, 0), Quaternion.identity);
                    }
                }
                Console.WriteLine(e);
                goto baslangic;
                throw;
            }
            _slider.transform.SetParent(canvas.transform);
        }

        
    }
}