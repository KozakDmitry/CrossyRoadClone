using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingObject : MonoBehaviour, IPoolObj
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isRift;


    public ObjectPool.ObjectInfo.ObjType Type => type;

    [SerializeField]
    private ObjectPool.ObjectInfo.ObjType type;

    [SerializeField]
    private float lifetime = 8f;
    private float currentLifeTime;


    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        currentLifeTime = lifetime;
    }

    void Start()
    {
        
    }
    public bool IsItRift()
    {
        return isRift;
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if ((currentLifeTime -= Time.deltaTime) < 0)
        {
            ObjectPool.instance.DestroyObject(gameObject);
            
        }
    }
}
