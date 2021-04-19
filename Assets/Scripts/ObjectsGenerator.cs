using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ObjectsGenerator : MonoBehaviour
{
    [SerializeField]
    private List<ObjectPool.ObjectInfo.ObjType> types;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    [SerializeField]
    private bool isRightSide;

    private int choose;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnVehicle());
    }

 
    IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));
            choose = UnityEngine.Random.Range(0, types.Count);
            var gameObject = ObjectPool.instance.GetObject(types[choose]);
            gameObject.GetComponent<MovingObject>().OnCreate(spawnPosition.position, transform.rotation);
            if (isRightSide)
            {
               
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }

    void Update()
    {
        
    }
}
