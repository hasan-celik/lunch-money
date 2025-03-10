using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleUICanvas : MonoBehaviour
{
    [SerializeField] public Image roleImg;
    [SerializeField] public TMP_Text roleText;
    [SerializeField] public TMP_Text headerText;
    
    [SerializeField] public TMP_Text roleUI_Text;
    
    [SerializeField] private Sprite bullyImage;
    [SerializeField] private Sprite studentImage;
    
    

    // Update is called once per frame
    void Update()
    {
        if (roleUI_Text.text == "Student")
        {
            roleText.text = "STUDENT";
            roleImg.sprite = studentImage;
            roleText.color = Color.green;
            headerText.color = Color.green;
        }

        if (roleUI_Text.text == "Bully")
        {
            roleText.text = "BULLY";
            roleImg.sprite = bullyImage;
            roleText.color = Color.red;
            headerText.color = Color.red;
        }
    }

    private void Start()
    {
        Invoke("selfDisable",10);
    }

    public void selfDisable()
    {
        gameObject.SetActive(false);
    }
}
