using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRot;
    float yRot;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input) {
        float mouseX = input.x * Time.deltaTime * sensX;
        float mouseY = input.y * Time.deltaTime * sensY;
        yRot += mouseX;
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot+90f, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);

    }
}