using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleUIColor : MonoBehaviour
{
    public TMP_Text roleUI;
    public RawImage image;

    private void Update()
    {
        string role = roleUI.text;
        switch (role)
        {
            case "Student":
                image.color = Color.green;
                break;
            case "Bully":
                image.color = Color.red;
                break;
            case "Ghost":
                image.color = Color.cyan;
                break;
            default:
                break;
        }
    }
}
