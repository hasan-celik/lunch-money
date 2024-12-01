using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviourPunCallbacks
{
    public PlayerRole role;
    [SerializeField] private BullyBehavior bullyBehavior;
    [SerializeField] private TMP_Text PlayerRoleText;

    public List<GameObject> zorbaGameObjets;

    void Start()
    {
        PlayerRoleText = GameObject.FindGameObjectWithTag("RoleUI").GetComponent<TMP_Text>();
        zorbaGameObjets = GameObject.FindGameObjectsWithTag("Zorba").ToList();
        AssignRole();
    }

    private void Update()
    {
        if (role == PlayerRole.Zorba)
        {
            PlayerRoleText.text = "Zorba";
            bullyBehavior.enabled = true;
            foreach (GameObject obj in zorbaGameObjets) 
            {
                obj.SetActive(true);
            }
        }
        else 
        {
            PlayerRoleText.text = "Ogrenci";
            bullyBehavior.enabled=false;
            foreach (GameObject obj in zorbaGameObjets) 
            {
                obj.SetActive(false);
            }
        }
    }

    void AssignRole()
    {
        // Rastgele bir rol atama
        role = (Random.value > 0.3f) ? PlayerRole.Ogrenci : PlayerRole.Zorba;

        // Rol ile ilgili özellikleri ayarlayın
        
    }
}
