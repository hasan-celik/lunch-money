using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject gm;

    public List<GameObject> players;

    [SerializeField] private GameObject miniMap;
    [SerializeField] private GameObject roleUI;
    [SerializeField] private GameObject taskSlider;
    [SerializeField] private GameObject skillButton;
    [SerializeField] private GameObject interactButton;
    [SerializeField] private GameObject renkButton;

    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            miniMap.SetActive(true);
            roleUI.SetActive(true);
            taskSlider.SetActive(true);
            skillButton.SetActive(true);
            interactButton.SetActive(true);
            foreach (var p in players) 
            {
                p.GetComponent<PlayerScript>().enabled = true;
                p.GetComponentInChildren<Yakinlik>().enabled = true;
                p.GetComponent<Transform>().position = new Vector3(Random.Range(-18,18), Random.Range(-18, 18), p.GetComponent<Transform>().position.z);
            }

            gm.SetActive(true);
            renkButton.SetActive(false);
            startButton.gameObject.SetActive(false);
        });
    }

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
    }
}
