using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class DragAndDropManager : MonoBehaviourPunCallbacks, IDropHandler
{
    public Slider targetSlider; // Hedef slider

    public void OnDrop(PointerEventData eventData)
    {
        // Sürüklenen nesneyi al
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // DragableScript bileşenine sahip olup olmadığını kontrol et
            Dragable dragable = draggedObject.GetComponent<Dragable>();
            if (dragable != null)
            {
                // Nesneyi yok et
                Destroy(draggedObject);

                // Slider değerini artır
                IncreaseSliderValue();
            }
        }
    }

    private void IncreaseSliderValue()
    {
        // Slider değerini artır
        if (targetSlider != null)
        {
            targetSlider.value += 1; // Değerini 1 artır
            // Photon ile diğer oyunculara güncellemeyi bildir
            //photonView.RPC("SyncSliderValue", RpcTarget.All, targetSlider.value);
            SyncSliderValue(targetSlider.value);
        }
    }

    //[PunRPC]
    private void SyncSliderValue(float newValue)
    {
        // Slider değerini senkronize et
        targetSlider.value = newValue;
    }
}
