using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour {
    [SerializeField] private Transform canParent;
    [SerializeField] private LayerMask noLayer;
    [SerializeField] private int objCount;
    private void Start() {
        for (int i = 0; i <= objCount; i++) {
            Spawn();
        }
    }

    private void Spawn() {
        GameObject obj = ObjectPooler.instance.GetPooledObject();

        Vector3 randomPoint = new Vector3(Random.Range(-15f, 15f), 0.1f, Random.Range(-15f, 15f));

        obj.transform.position = randomPoint;
        obj.transform.parent = canParent;
        obj.SetActive(true);
    }
}