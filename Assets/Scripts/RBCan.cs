using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBCan : MonoBehaviour {
    [SerializeField] private GameObject can;

    private void Start() {
        Invoke(nameof(TurnIntoCan), 2.5f); // After 2.5 seconds have passed from the RBCan Initiation, destroy it, and spawn a Can gameobject instead of RBCan that can be picked up
    }

    private void TurnIntoCan() {
        Instantiate(can, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}