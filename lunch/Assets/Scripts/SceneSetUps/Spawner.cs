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

    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();

        ColorPickerButton.onClick.AddListener(() =>
        {
            ColorPanel();
        });
        ColorButtons = GameObject.FindGameObjectsWithTag("ColorButton").ToList();

        // if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        // {
        //     GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-8, 8), 1.5f, 0), quaternion.identity);
        // }
        SpawnObject();
    }
    
    public void SpawnObject()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer != null)
        {
            Color colorToSpawn = GetRandomColor();
            if (colorToSpawn != Color.clear) 
            {
                GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-68, -58), 1.5f, 0), quaternion.identity);

                //photonView.RPC("ColorSet", RpcTarget.All, player, colorToSpawn);
                player.GetComponent<Renderer>().material.color = colorToSpawn;
                usedColors.Add(colorToSpawn); 
            }
            else
            {
                Debug.Log("Lobi full dolu");
            }
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

        return Color.clear; // Tüm renkler kullanıldıysa
    }

    public void ColorSet(GameObject p, Color color)
    {
        p.GetComponent<Renderer>().material.color = color;
    }

    //private void Update()
    //{
    //    for (int i = 0; i < 20; i++)
    //    {
    //        for (int j = 0; j < 20; j++)
    //        {
    //            ColorButtons[i].GetComponent<Button>().colors.normalColor;
    //        }
    //    }
    //}

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
}