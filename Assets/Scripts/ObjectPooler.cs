using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    #region Singleton

    public static ObjectPooler instance;

    private void Awake() {
        instance = this;
    }

    #endregion

    public GameObject objectToPool;
    public int poolSize;
    private List<GameObject> pooledObjects;

    private void Start() {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }

        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false);
        pooledObjects.Add(obj);

        return obj;
    }
}