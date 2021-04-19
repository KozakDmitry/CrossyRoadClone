using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    private List<ObjectInfo> objectInfo;

    [Serializable]
    public struct ObjectInfo
    {
        public enum ObjType
        {
            Vehicle_1,
            Vehicle_2,
            Rift,
            Train,
            Tree,
            Stump,
            Fir,
            Coin
        }

        public ObjType Type;
        public GameObject Prefab;
        public int StartCount;
    }



    private Dictionary<ObjectInfo.ObjType, Pool> pools;

    private void InitPool()
    {
        pools = new Dictionary<ObjectInfo.ObjType, Pool>();
        var emptyGameObject = new GameObject();


        foreach (var obj in objectInfo)
        {
            var container = Instantiate(emptyGameObject, transform, false);
            container.name = obj.Type.ToString();

            pools[obj.Type] = new Pool(container.transform);

            for (int i = 0; i < obj.StartCount; i++)
            {
                var gameObj = InstantiateObject(obj.Type, container.transform);
                pools[obj.Type].Objects.Enqueue(gameObj);
            }
        }
        Destroy(emptyGameObject);
    }


    private GameObject InstantiateObject(ObjectInfo.ObjType type, Transform parent)
    {
        var gameObject = Instantiate(objectInfo.Find(x => x.Type == type).Prefab, parent);
        gameObject.SetActive(false);
        return gameObject;
    }


    public GameObject GetObject(ObjectInfo.ObjType type)
    {
        var obj = pools[type].Objects.Count > 0 ? pools[type].Objects.Dequeue() : InstantiateObject(type, pools[type].Container);

        obj.SetActive(true);
        return obj;
    }

    public void DestroyObject(GameObject obj)
    {
        pools[obj.GetComponent<IPoolObj>().Type].Objects.Enqueue(obj);
        obj.SetActive(false);
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        InitPool();
    }


    
}
