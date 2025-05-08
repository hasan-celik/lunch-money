using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviourPunCallbacks
{
    [PunRPC]
    private void IncreaseSliderValue()
    {
        this.GetComponent<Slider>().value += 1;
    }

    public void sliderValueChanged()
    {
        photonView.RPC("IncreaseSliderValue", RpcTarget.AllBuffered);
    }
}
