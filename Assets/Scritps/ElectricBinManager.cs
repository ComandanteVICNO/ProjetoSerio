using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBinManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DragAndDrop dragNDrop;
    public GameObject particleEffect;
    public Transform particleSource;

    public Material particleMaterial;
    public Material particleEmissionMaterial;

    public Color particleColor = new Color(227, 35, 19, 255);
    public float emissionIntensity;
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
            if (other.CompareTag("Electric"))
            {
                scoreManager.IncreaseScore();
                Object.Destroy(other.gameObject);
                SpawnParticles();
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

    public void SpawnParticles()
    {

        particleMaterial.color = particleColor;
        particleEmissionMaterial.SetColor("_EmissionColor", particleColor);

        GameObject particlesObject = Instantiate(particleEffect, particleSource);

    }
}
