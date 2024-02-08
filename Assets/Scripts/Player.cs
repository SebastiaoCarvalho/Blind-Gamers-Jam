using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 5f, cameraSpeed = 20;
    [SerializeField] private InputActionReference movementAction, cameraAction, dogCallAction, dogFindAction, interactAction;
    private GameObject dog;
    private List<GameObject> interactibles = new List<GameObject>();
    private Rigidbody rb;
    private float rotationTarget; // target rotation position
    private float initialRotation; // initial rotation position
    private float rotationMovement; // sign of camera rotation

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dog = GameObject.Find("GuideDog");
        initialRotation = transform.rotation.eulerAngles.y;
        rotationTarget = transform.rotation.eulerAngles.y;
        rotationMovement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        // Player movement
        Vector2 movementInput = movementAction.action.ReadValue<Vector2>();

        // move according to transform direction
        Vector3 movement3D = (movementInput.x * transform.right) + (movementInput.y * transform.forward);
        movement3D.Normalize();

        rb.velocity = new Vector3(movement3D.x  * speed, rb.velocity.y, movement3D.z * speed);

        // Camera rotation
        Rotation();
    }

    private void Rotation() {
        float rotation = cameraAction.action.ReadValue<float>();

        rotation = Math.Sign(rotation);
        if (rotation != rotationMovement && rotation != 0) {
            if (rotationMovement != 0) {
                initialRotation = rotationTarget;
            }
            else initialRotation = transform.rotation.eulerAngles.y;
            rotationMovement = rotation;
            rotationTarget = initialRotation + rotation * 90;
            if (rotationTarget < 0) { 
                rotationTarget += 360;
                initialRotation += 360;
            }
            else if (rotationTarget >= 360) {
                rotationTarget -= 360;
                initialRotation -= 360;
            }
        }
        
        if (transform.rotation.eulerAngles.y != rotationTarget) {
            float start = transform.rotation.eulerAngles.y == 0 && initialRotation == 360 ? 360 : transform.rotation.eulerAngles.y;
            float end = rotationTarget == 0 ? (transform.rotation.eulerAngles.y <= 90 ? 0 : 360) : rotationTarget;
            transform.rotation = Quaternion.Euler(0, Mathf.MoveTowards(start, end, cameraSpeed * Time.fixedDeltaTime), 0);
        }
        else {
            rotationMovement = 0;
        }
    }

    private void OnEnable() {
        dogCallAction.action.performed += DogCallAction;
        dogFindAction.action.performed += DogFindAction;
        interactAction.action.performed += InteractAction;
    }

    private void OnDisable() {
        dogCallAction.action.performed -= DogCallAction;
        dogFindAction.action.performed -= DogFindAction;
        interactAction.action.performed -= InteractAction;
    }

    private void DogCallAction(InputAction.CallbackContext obj) {
        dog.GetComponent<Dog>().HearCall();
    }
    
    private void DogFindAction(InputAction.CallbackContext obj) {
        dog.GetComponent<Dog>().FindClue();
    }
    
    private void InteractAction(InputAction.CallbackContext obj) {
        if (interactibles.Count == 0) return;
        GameObject closest = interactibles[0];
        float minDistance = Vector3.Distance(transform.position, closest.transform.position);
        foreach (GameObject interactible in interactibles) {
            float distance = Vector3.Distance(transform.position, interactible.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                closest = interactible;
            }
        }
        closest.GetComponent<Interactible>().Interact();
    }

    public void AddInteractible(GameObject interactible) {
        interactibles.Add(interactible);
    }

    public void RemoveInteractible(GameObject interactible) {
        interactibles.Remove(interactible);
    }

}
