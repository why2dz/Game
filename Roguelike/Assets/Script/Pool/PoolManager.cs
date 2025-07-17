using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 管理器类，创建销毁必备
/// </summary>
[Serializable]
public struct Pool
{
    public int PoolSize;
    public GameObject prefab;
    public string componentType;

}
[DisallowMultipleComponent]
public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private Pool[] pools = null;

    private Transform objectPoolTransform;

    public Dictionary<int, Queue<Component> > poolDictionary = new Dictionary<int, Queue<Component>>();

    private void Start()
    {
        objectPoolTransform = this.gameObject.transform;
        for (int i = 0; i < pools.Length; i++)
        {
            CreatePool(pools[i].prefab, pools[i].PoolSize, pools[i].componentType);
        }

    }

    private void CreatePool(GameObject prefab, int poolSize, string componentType)
    {
        int poolKey = prefab.GetInstanceID();
        string prefabName = prefab.name;

        GameObject parentGameobjct = new GameObject(prefabName + "Anchor");

        parentGameobjct.transform.SetParent(objectPoolTransform);

        if (!poolDictionary.ContainsKey(poolKey))
        {
            poolDictionary.Add(poolKey, new Queue<Component>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObjcet = Instantiate(prefab, parentGameobjct.transform);
                newObjcet.SetActive(false);
                poolDictionary[poolKey].Enqueue(newObjcet.GetComponent(Type.GetType(componentType)));
            }
        }
    }

    public Component ReuseComponent(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (poolDictionary.ContainsKey(poolKey))
        {
            Component componentToReuse = GetComponentFormPool(poolKey);
            ResetObject(position, rotation, componentToReuse, prefab);
            return componentToReuse;
        }
        else
        {
            Debug.Log("没有这个对象池");
            return null;
        }
    }


    private Component GetComponentFormPool(int poolKey)
    {
        Component Reusecomponent =  poolDictionary[poolKey].Dequeue();
        poolDictionary[poolKey].Enqueue(Reusecomponent);
        if (Reusecomponent.gameObject.activeSelf == true)
        {
            Reusecomponent.gameObject.SetActive(false);
        }
        return Reusecomponent;
    }

    private void ResetObject(Vector3 position, Quaternion rotation, Component componentToReuse, GameObject prefab)
    {
        componentToReuse.transform.position = position;
        componentToReuse.transform.rotation = rotation;
        componentToReuse.gameObject.transform.localScale = prefab.transform.localScale;


    }
}
