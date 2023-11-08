using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBinManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DragAndDrop dragNDrop;
    StopClock stopClock;
    public GameObject pauseCanvas;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<DragAndDrop>() == null) return;
        dragNDrop = other.GetComponent<DragAndDrop>();

        if (other.GetComponent<StopClock>() != null) 
        {
            stopClock = other.GetComponent<StopClock>();
            stopClock.canStop = true;
        }


        if (dragNDrop.isDragging) return;
        else
        {
            if (other.CompareTag("Plastic") || other.CompareTag("Metal"))
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
        stopClock.canStop = false;
        stopClock = null;
        
    }

    private void Pause()
    {
        pauseCanvas.SetActive(true);
    }
}
