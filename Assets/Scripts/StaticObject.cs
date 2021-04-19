using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObject : MonoBehaviour,IPoolObj
{
    public ObjectPool.ObjectInfo.ObjType Type => type;
    [SerializeField]
    private ObjectPool.ObjectInfo.ObjType type;




    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Destroy()
    {
        ObjectPool.instance.DestroyObject(gameObject);
    }
}
