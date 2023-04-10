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

    [SerializeField] private GameObject objectToPool;
    private int poolSize = 10;
    private List<GameObject> pooledObjects;

    private void Start() {
        poolSize = LevelData.canPoolSize;

        pooledObjects = new List<GameObject>();

        // Instantiate a can object {poolSize} times, set its parent to this.transform, and deactivate it
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(objectToPool);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject() {
        // Return an available gameObject from the objectPool
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