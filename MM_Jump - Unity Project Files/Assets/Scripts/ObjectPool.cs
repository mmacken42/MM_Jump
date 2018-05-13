using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject pooledObject;

    public int poolSize;

    private List<GameObject> pool;

	void Start ()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; ++i)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);

            obj.SetActive(false);

            pool.Add(obj);
        }
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; ++i)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        GameObject obj = (GameObject)Instantiate(pooledObject);

        obj.SetActive(false);

        pool.Add(obj);

        return obj;
    }
}
