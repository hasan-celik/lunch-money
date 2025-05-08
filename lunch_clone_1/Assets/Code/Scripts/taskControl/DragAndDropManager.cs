using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Rendering.Universal;

public class DragAndDropManager : MonoBehaviourPunCallbacks, IDropHandler
{
    public Slider targetSlider; 
    [SerializeField] TaskManager taskManager;
    
    [SerializeField] private AudioClip _rightAudioClip;
    [SerializeField] private AudioClip _wrongAudioClip;
    private GameObject gorev;

    private void Start()
    {
        gorev = taskManager.gameObject;
    }

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
                    AudioSource TaskAudioSource = gorev.GetComponent<AudioSource>();
                    TaskAudioSource.PlayOneShot(_rightAudioClip);
                    gorev.GetComponent<Light2D>().enabled =  false;
                    gorev.GetComponent<SpriteRenderer>().color =  Color.white;
                    gorev.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    targetSlider.GetComponent<SliderScript>().sliderValueChanged();
                    taskManager.ToggleTaskPanel();
                }
                else
                {
                    AudioSource TaskAudioSource = gorev.GetComponent<AudioSource>();
                    TaskAudioSource.PlayOneShot(_wrongAudioClip);
                    taskManager.ToggleTaskPanel();
                }
            }
        }
    }
}
