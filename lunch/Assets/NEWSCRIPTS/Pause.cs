using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public void deactivePlayerMovement() 
    {
        _playerMovement.enabled = false;
    }
    
    public void activePlayerMovement() 
    {
        _playerMovement.enabled = true;
    }
}
