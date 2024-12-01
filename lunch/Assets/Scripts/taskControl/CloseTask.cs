using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTask : MonoBehaviour
{
    private Button CloseTaskButton;

    private void Awake()
    {
        CloseTaskButton = GetComponentInChildren<Button>();
        CloseTaskButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
