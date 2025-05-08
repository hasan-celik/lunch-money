using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HatHolder : MonoBehaviourPun
{
    public Hats selectedHat;
    [SerializeField] public GameObject[] hats;
    [SerializeField] private GameObject hatholder;

    private GameObject currentHatInstance;
    private PlayerMovement playerMovement;

    void Start()
    {
        if (photonView.IsMine)
        {
            playerMovement = GetComponent<PlayerMovement>(); // PlayerMovement referansını al
            UpdateHat();
        }
    }
    
    [PunRPC]
    public void SetHat(Hats newHat)
    {
        if (selectedHat == newHat) return; // Aynı şapka seçildiyse işlem yapma

        selectedHat = newHat;

        // Seçilen şapkayı tüm oyunculara bildir
        photonView.RPC("RPC_UpdateHat", RpcTarget.AllBuffered, (int)newHat);
    }

    [PunRPC]
    private void RPC_UpdateHat(int hatIndex)
    {
        selectedHat = (Hats)hatIndex; // Gelen index'i enum'a çevir
        UpdateHat(); // Şapkayı güncelle
    }

    private void UpdateHat()
    {
        // Önce eski şapkayı temizle
        foreach (Transform child in hatholder.transform)
        {
            Destroy(child.gameObject);
        }

        // Eğer "None" seçildiyse (index == 40), yeni obje oluşturma
        if ((int)selectedHat == 40) return;

        // Eğer geçerli bir şapka seçildiyse, instantiate et
        if ((int)selectedHat >= 0 && (int)selectedHat < hats.Length)
        {
            currentHatInstance = Instantiate(hats[(int)selectedHat], hatholder.transform);
        }
    }

    void Update()
    {
        if (photonView.IsMine && playerMovement != null && hatholder != null)
        {
            // Eğer player flip olmuşsa, şapkayı da çevir
            hatholder.transform.localScale = new Vector3(playerMovement.GetFlipX() ? -1 : 1, 1, 1);
        }
    }
}