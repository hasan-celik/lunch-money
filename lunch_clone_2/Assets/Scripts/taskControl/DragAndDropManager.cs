using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class DragAndDropManager : MonoBehaviourPunCallbacks, IDropHandler
{
    public Slider targetSlider; 
    public void OnDrop(PointerEventData eventData)
    {
        targetSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            Dragable dragable = draggedObject.GetComponent<Dragable>();
            if (dragable != null)
            {
                Destroy(draggedObject);
                if (draggedObject.CompareTag("rightAnswer"))
                {
                    targetSlider.GetComponent<SliderScript>().sliderValueChanged();
                }
            }
        }
    }
}
