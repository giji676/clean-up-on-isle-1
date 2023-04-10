using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour {
    [SerializeField] private Transform canParent;
    [SerializeField] private LayerMask shelfLayer;
    private int cansNumToSpawn = 10;
    private int numSpawned = 0;
    private Vector3 checkBoxSize = new Vector3(0.01f, 0.01f, 0.01f);

    private void Start() {
        cansNumToSpawn = LevelData.cansNumToSpawn;
    }

    private void Update() {
        // Each frame spawn one can, I am doing this over multiple frames instead on in the Start function becuase in Start it can cause error because of the amound of collision checks
        if (numSpawned < cansNumToSpawn) {
            Spawn();
            numSpawned++;
        }
    }

    private void Spawn() {
        // Get one can object from the ObjectPool
        GameObject obj = ObjectPooler.instance.GetPooledObject();

        // Generate random point on the scene
        Vector3 randomPoint = new Vector3(Random.Range(-15f, 15f), 0.1f, Random.Range(-15f, 15f));
        // Check if the can is overlapping with Shelves
        bool overlaps = Physics.CheckBox(randomPoint, checkBoxSize, Quaternion.identity, shelfLayer);
        
        if (overlaps) {
            numSpawned--;
            return;
        }

        // If it's not overlapping sets its position, parent and set it active
        obj.transform.position = randomPoint;
        obj.transform.parent = canParent;
        obj.SetActive(true);
    }
}