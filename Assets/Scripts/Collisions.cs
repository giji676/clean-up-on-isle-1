using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour {
    [SerializeField] private LayerMask canLayer;
    [SerializeField] private LayerMask spillLayer;
    [SerializeField] private LayerMask sellPointLayer;
    [SerializeField] private Transform[] canPos; // The transforms of where the cans will be moved to once picked up. It must be the same number as maxStorage
    [SerializeField] private Transform[] cans; // Cannot be more then canPos and maxStorage
    [SerializeField] private Transform canSpawn; // Where the rigidbody Can will be spawned after collision witht the spill
    [SerializeField] private GameObject rbCan; // Rigidbody Can that will be spawned and be throw out of the cart
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private int maxStorage; // Maximum amount of cans that can be picked up by the cart
    [SerializeField] private AudioSource sellAudio;
    [SerializeField] private AudioSource canPickupSound;
    [SerializeField] private AudioSource canPopSound;

    private int cansPickedUp = 0;
    private PlayerMotor playerMotor;
    private PlayerUI playerUI;
    private int cansSold = 0;
    private GameManager gameManager;

    private void Start() {
        playerMotor = GetComponent<PlayerMotor>();
        playerUI = GetComponent<PlayerUI>();
        cans = new Transform[maxStorage];
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider collider) {
        // If player is colliding with canLayer
        if (((1 << collider.gameObject.layer) & canLayer.value) != 0) {
            if (cansPickedUp >= maxStorage) return;

            collider.isTrigger = false;
            AddCan(collider.transform);
        }
        // If player is colliding with spillLayer
        else if (((1 << collider.gameObject.layer) & spillLayer.value) != 0) {
            StartCoroutine(cameraShake.Shake(.2f, .09f));
            Slip();
        }
        // If player is colliding with sellPointLayer
        else if (((1 << collider.gameObject.layer) & sellPointLayer.value) != 0) {
            Sell();
        }
    }

    private void AddCan(Transform canTransform) {
        // Add the can to the cart
        canTransform.position = canPos[cansPickedUp].position;
        canTransform.rotation = canPos[cansPickedUp].rotation;
        canTransform.parent = canPos[cansPickedUp];
        cans[cansPickedUp] = canTransform;

        cansPickedUp++;
        
        // Add more drag to slow down the cart
        playerMotor.UpdateDrag(cansPickedUp);
        canPickupSound.Play();
    }

    private void Slip() {
        // If there is nothing in the cart return
        if (cansPickedUp == 0) return;
        playerUI.DamageOverlay();
        
        // Spawn a rigidbody can so physics can be applied
        GameObject newCan = Instantiate(rbCan, canSpawn.position, transform.rotation);
        Rigidbody rbNewCan = newCan.GetComponent<Rigidbody>();
        // Apply upward force and random direction force
        rbNewCan.AddForce(Vector3.up * 4 + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * 2 , ForceMode.Impulse);
        
        // Destroy the can in the cart and remove it from the list
        Destroy(cans[cansPickedUp-1].gameObject);
        cans[cansPickedUp-1] = null;
        
        cansPickedUp--;

        playerMotor.UpdateDrag(cansPickedUp);
        canPopSound.Play();
    }

    private void Sell() {
        // If there is nothing in the cart return
        if (cansPickedUp == 0) return;

        // Destroy each can in the cart and remove it from the list
        for (int i = 0; i < cans.Length; i++) {
            if (cans[i] == null) continue;

            Destroy(cans[i].gameObject);
            cans[i] = null;

            cansSold++;
            cansPickedUp--;
        }
        gameManager.UpdateSales(cansSold);
        cansSold = 0;
        
        playerMotor.UpdateDrag(cansPickedUp);
        sellAudio.Play();
    }
}