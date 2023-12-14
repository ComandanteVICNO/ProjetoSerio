using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganicBinManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DragAndDrop dragNDrop;
    private void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        dragNDrop = FindAnyObjectByType<DragAndDrop>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DragAndDrop>() == null) return;
        dragNDrop = other.GetComponent<DragAndDrop>();

        if (dragNDrop.isDragging) return;
        else
        {
            if (other.CompareTag("Organic"))
            {
                scoreManager.IncreaseScore();
                Object.Destroy(other.gameObject);
            }
            else
            {
                scoreManager.DecreaseScore();
                Object.Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dragNDrop = null;   
    }
}
