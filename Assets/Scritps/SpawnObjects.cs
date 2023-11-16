using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    int randomIndex;
    Coroutine coroutine;
    public ScoreManager scoreManager;

    [Header("Objects to Spawn")]
    public GameObject[] paperObjects;
    public GameObject[] glassObjects;
    public GameObject[] plasticAndMetalObjects;
    GameObject objectToSpawn;
    private string selectedCategory;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    public Transform selectedSpawnPoint;


    [Header("Values")]
    public float minSpawnTime;
    public float maxSpawnTime;
    public float spawnCheckRadius;
    public float spawnTime;


    
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        coroutine = StartCoroutine(SpawnObject());


    }

    private void Update()
    {
        DoSpawnCoroutine();
        
    }

    void DoSpawnCoroutine()
    {
        if (scoreManager.newElapsedTime <= 0) return;
        else
        {
            if (coroutine != null) return;
            else
            {
                coroutine = StartCoroutine(SpawnObject());
            }
        }
    }

    IEnumerator SpawnObject()
    {
        Debug.Log("Started");
        SetRandomCooldown();
        SelectSpawnPoint();
        SelectObjectInCategory();


        yield return new WaitForSecondsRealtime(spawnTime);
        Debug.Log("waited");

        if (selectedSpawnPoint != null && objectToSpawn != null)
        {
            if (!IsOccupied(selectedSpawnPoint.position))
                Instantiate(objectToSpawn, selectedSpawnPoint.position, Quaternion.identity);

        }

         
        

        objectToSpawn = null;
        RemoveSelectedSpawnPoint();
        coroutine = null;
        Debug.Log("finished");
    }

    public void SetRandomCooldown()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    

    

    #region SpawnLocationSelection
    public Transform SelectSpawnPoint()
    {

        int randomIndex = Random.Range(0, spawnPoints.Length);

        selectedSpawnPoint = spawnPoints[randomIndex];

        randomIndex = -1;

        return selectedSpawnPoint;

    }


    public void RemoveSelectedSpawnPoint()
    {
        selectedSpawnPoint = null;
    }
    #endregion

    #region SpawnObjectSelection
    private void SelectCategory()
    {
        int randomNum = Random.Range(1, 4);

        switch (randomNum)
        {
            case 1:
                selectedCategory = "Paper";
                break;
            case 2:
                selectedCategory = "Glass";
                break;
            case 3:
                selectedCategory = "PlasticAndMetal";
                break;
        }

        randomNum = -1;
    }

    public GameObject SelectObjectInCategory()
    {
        SelectCategory();
        int randomIndex;
        switch (selectedCategory)
        {
            case "Paper":
                randomIndex = Random.Range(0, paperObjects.Length );
                objectToSpawn = paperObjects[randomIndex];

                break;

            case "Glass":

                randomIndex = Random.Range(0, glassObjects.Length );
                objectToSpawn = glassObjects[randomIndex];

                break;

            case "PlasticAndMetal":

                randomIndex = Random.Range(0, plasticAndMetalObjects.Length);
                objectToSpawn = plasticAndMetalObjects[randomIndex];

                break;
        }

        return objectToSpawn;
    }
    #endregion

    private bool IsOccupied(Vector3 position)
    {
        
        Collider[] colliders = Physics.OverlapSphere(position, spawnCheckRadius);

        
        return colliders.Length > 0;
    }
}
