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
    [SerializeField] private InputActionReference movementAction, cameraAction, dogCallAction, dogFindAction;
    private GameObject dog;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dog = GameObject.Find("GuideDog");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Vector2 movementInput = movementAction.action.ReadValue<Vector2>();

        // move according to transform direction
        Vector3 movement3D = (movementInput.x * transform.right) + (movementInput.y * transform.forward);
        movement3D.Normalize();

        rb.velocity = new Vector3(movement3D.x  * speed, rb.velocity.y, movement3D.z * speed);
        float rotation = cameraAction.action.ReadValue<float>();
        transform.Rotate(Vector3.up, rotation * cameraSpeed);
    }

    private void OnEnable() {
        dogCallAction.action.performed += DogCallAction;
        dogFindAction.action.performed += DogFindAction;
    }

    private void OnDisable() {
        dogCallAction.action.performed -= DogCallAction;
        dogFindAction.action.performed -= DogFindAction;
    }

    private void DogCallAction(InputAction.CallbackContext obj) {
        dog.GetComponent<Dog>().HearCall();
    }
    
    private void DogFindAction(InputAction.CallbackContext obj) {
        dog.GetComponent<Dog>().FindClue();
    }

}
