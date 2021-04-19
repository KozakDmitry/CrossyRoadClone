using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private List<ObjectPool.ObjectInfo.ObjType> typesOfObjects;
    private Vector3 currentPosition = new Vector3(1, 0, 0);
    [SerializeField]
    public int minGeneration;
    [SerializeField]
    private int terrainWithoutTrain;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private List<TemplateTerrain> terrains = new List<TemplateTerrain>();
    [SerializeField]
    private GameObject pl;




    private List<GameObject> currentTerrain = new List<GameObject>();
    private int trainCooldown=0;
    private int randomTerrain;
    private int placeForObjects;
    private int randomObject;
    
   
    public void StartGeneration()
    {
        while (true)
        {
            if (currentPosition.x - pl.transform.position.x < minGeneration)
            {
                SpawnTerrain();
            }
            else
            {
                break;
            }
        }
    }

    private void Start()
    {
        StartGeneration();
        Instantiate(terrains[0].terrainType[0], currentPosition, Quaternion.identity);
        currentPosition.x++;

    }
    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (currentPosition.x - pl.transform.position.x < minGeneration)
                {
                    SpawnTerrain();
                }
                if (Mathf.Abs(currentTerrain[0].transform.position.x - pl.transform.position.x) > 7)
                {
                    Destroy(currentTerrain[0]);
                    currentTerrain.RemoveAt(0);
                }

            }
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{

        //    if (currentPosition.x - pl.transform.position.x < 24)
        //    {
        //        SpawnTerrain();
        //    }
        //    if (Mathf.Abs(currentTerrain[0].transform.position.x - pl.transform.position.x) > 7)
        //    {
        //        Destroy(currentTerrain[0]);
        //        currentTerrain.RemoveAt(0);
        //    }
        //}
    }
    private void SpawnObjects()
    {
        switch (randomTerrain)
        {
            case 0:
                
                placeForObjects = Random.Range(-gm.GetAbleMove(), gm.GetAbleMove());
                randomObject = Random.Range(0, typesOfObjects.Count-1);
                for (int i = 0; i < Random.Range(2, 3);i++) 
                {
                    var gameObject = ObjectPool.instance.GetObject(typesOfObjects[randomObject]);
                    gameObject.GetComponent<StaticObject>().OnCreate(currentPosition + new Vector3(0, 0, placeForObjects), transform.rotation);
                }
                SpawnCoins();
                break;
            case 2:
                break;
            default:
                break;
        }
            
    }

    private void SpawnCoins()
    {
        for(int i =0;i< Random.Range(0, 2); i++)
        {
            var gameObject = ObjectPool.instance.GetObject(typesOfObjects[typesOfObjects.Count-1]);
            gameObject.GetComponent<StaticObject>().OnCreate(currentPosition + new Vector3(0, (float)0.5, placeForObjects),Quaternion.Euler(new Vector3(90f,0,0)));
        }
    }
    private void SpawnTerrain()
    {
        randomTerrain = Random.Range(0, terrains.Count);
        if (randomTerrain == terrains.Count-1)
        {
            if (trainCooldown < terrainWithoutTrain)
            {
                randomTerrain = Random.Range(0, terrains.Count-1);
                trainCooldown++;
            }
            else
            {
                trainCooldown = 0;
            }
            
        }
        int typeOfTerrain = Random.Range(0, terrains[randomTerrain].terrainType.Count);
        int CountofTerrain = Random.Range(0, terrains[randomTerrain].maxTerrainInOneTime);
        
        for (int i = 0; i <= CountofTerrain; i++)
        {
            GameObject terr = Instantiate(terrains[randomTerrain].terrainType[typeOfTerrain], currentPosition, Quaternion.identity);
            currentTerrain.Add(terr);
            SpawnObjects();
            currentPosition.x++;
        }

    }
}
