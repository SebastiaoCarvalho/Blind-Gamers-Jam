using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 5f, cameraSpeed = 5f;
    [SerializeField] private InputActionReference movementAction, cameraAction;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() { // FIXME : rotatated object not moving to the right direction -> see yt video
        Vector2 movementInput = movementAction.action.ReadValue<Vector2>();
        rb.velocity = new Vector3(movementInput.x * speed, rb.velocity.y, movementInput.y * speed);
        /* float rotation = Math.Sign(cameraAction.action.ReadValue<float>()); */
        float rotation = cameraAction.action.ReadValue<float>();
        Debug.Log(rotation);
        transform.Rotate(Vector3.up, rotation * cameraSpeed);
        
    }
}
