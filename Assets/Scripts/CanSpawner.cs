using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour {
    [SerializeField] private Transform canParent;
    [SerializeField] private LayerMask shelfLayer;
    [SerializeField] private int objCount;
    private int numSpawned = 0;
    private Vector3 checkBoxSize = new Vector3(0.01f, 0.01f, 0.01f);

    private void Update() {
        if (numSpawned < objCount) {
            Spawn();
            numSpawned++;
        }
    }

    private void Spawn() {
        GameObject obj = ObjectPooler.instance.GetPooledObject();

        Vector3 randomPoint = new Vector3(Random.Range(-15f, 15f), 0.1f, Random.Range(-15f, 15f));
        bool overlaps = Physics.CheckBox(randomPoint, checkBoxSize, Quaternion.identity, shelfLayer);
        
        if (overlaps) {
            numSpawned--;
            return;
        }

        obj.transform.position = randomPoint;
        obj.transform.parent = canParent;
        obj.SetActive(true);
    }
}