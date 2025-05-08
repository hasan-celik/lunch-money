using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerTasks : MonoBehaviourPunCallbacks
{
    public int taskCount = 30;
    public List<GameObject> allInteractableObjects;
    public List<GameObject> assignedObjects;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GamePlay" && photonView.IsMine)
        {
            FindInteractableObjects();
            AssignInteractableObjects();

            foreach (GameObject task in assignedObjects)
            {
                task.tag = "task";
                task.GetComponent<Light2D>().enabled =  true;
                task.GetComponent<SpriteRenderer>().color = Color.green;
                task.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                //task.GetComponent<TaskTrigger>().enabled = true;
            }
        }
    }


    void FindInteractableObjects()
    {
        allInteractableObjects = GameObject.FindGameObjectsWithTag("Interactable").ToList();
    }
    
    void AssignInteractableObjects()
    {
        assignedObjects = allInteractableObjects.OrderBy(x => Random.value).Take(taskCount).ToList();
    }
}

