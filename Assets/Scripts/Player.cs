using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 5f, cameraSpeed = 20;
    [SerializeField] private InputActionReference movementAction, cameraAction, dogCallAction, dogFindAction, interactAction;
    private InputActionMap soundMap;
    private GameObject dog;
    private List<GameObject> interactables = new List<GameObject>();
    private Rigidbody rb;
    private float rotationTarget; // target rotation position
    private float initialRotation; // initial rotation position
    private float movement; // sign of player movement
    private float rotationMovement; // sign of camera rotation
    private bool walking = false; // switch for walking sound
    private StudioEventEmitter walkingEventEmmiter;
    private StudioEventEmitter rotateEventEmmiter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dog = GameObject.Find("GuideDog");
        initialRotation = transform.rotation.eulerAngles.y;
        rotationTarget = transform.rotation.eulerAngles.y;
        rotationMovement = 0;
        walkingEventEmmiter = gameObject.GetComponents<StudioEventEmitter>()[0];
        rotateEventEmmiter = gameObject.GetComponents<StudioEventEmitter>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        // Player movement
        Movement();

        // Camera rotation
        Rotation();
    }

    private void Movement() {
        Vector2 movementInput = movementAction.action.ReadValue<Vector2>();
        /* movementInput = new Vector2(Mathf.Sign(movementInput.x), Mathf.Sign(movementInput.y)); */
        if (movementInput != Vector2.zero) {
            if (! walking) {
                walking = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Move/Player_Steps");

}
        }
        else {
            if (walking) {
                walking = false;
                walkingEventEmmiter.Stop();
                //FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Move/Turn Left");
            }
        }

        // move according to transform direction
        Vector3 movement3D = (movementInput.x * transform.right) + (movementInput.y * transform.forward);
        movement3D.Normalize();

        rb.velocity = new Vector3(movement3D.x  * speed, rb.velocity.y, movement3D.z * speed);
    }

    private void Rotation() {
        float rotation = cameraAction.action.ReadValue<float>();

        rotation = Math.Sign(rotation);
        if (rotation != rotationMovement && rotation != 0) {
            if (rotation > 0) {
                //rotateEventEmmiter.EventReference = EventReference.Find("event:/Sound Effects/Move/Turn Left");
                //rotateEventEmmiter.Play();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Move/Turn Right");
            }
            else {
                //rotateEventEmmiter.EventReference = EventReference.Find("event:/Sound Effects/Move/Turn Right");
                //rotateEventEmmiter.Play();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sound Effects/Move/Turn Left");
            }
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
        soundMap = GameObject.Find("PlayerInput").GetComponent<PlayerInput>().actions.FindActionMap("SequenceGameActions");
        for (int i = 0; i < soundMap.actions.Count; i++) {
            soundMap.actions[i].performed += PlaySound;
        }
    }

    private void OnDisable() {
        dogCallAction.action.performed -= DogCallAction;
        dogFindAction.action.performed -= DogFindAction;
        interactAction.action.performed -= InteractAction;
        for (int i = 0; i < soundMap.actions.Count; i++) {
            soundMap.actions[i].performed -= PlaySound;
        }
    }

    private void DogCallAction(InputAction.CallbackContext obj) {
        dog.GetComponent<Dog>().HearCall();
    }
    
    private void DogFindAction(InputAction.CallbackContext obj) {
        dog.GetComponent<Dog>().FindClue();
    }
    
    private void InteractAction(InputAction.CallbackContext obj) {
        if (interactables.Count == 0) return;
        GameObject closest = interactables[0];
        float minDistance = Vector3.Distance(transform.position, closest.transform.position);
        foreach (GameObject interactible in interactables) {
            float distance = Vector3.Distance(transform.position, interactible.transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                closest = interactible;
            }
        }
        closest.GetComponent<Interactable>().Interact();
    }

    public void AddInteractible(GameObject interactible) {
        interactables.Add(interactible);
    }

    public void RemoveInteractible(GameObject interactible) {
        interactables.Remove(interactible);
    }

    public void PlaySound(InputAction.CallbackContext obj) {
        int i = obj.action.name[^1] - '1'; // Convert last character of action name to int
        Debug.Log(obj.action.name + " " + i);
        if (interactables.Count == 0) return;
        string [] sounds = {"A", "B", "C", "D"};
        string sound = sounds[i];
        Debug.Log("Playing sound " + sound);
        MiniGameInteractable miniGameInteractable = interactables[0].GetComponent<MiniGameInteractable>();
        miniGameInteractable.TrySound(sound);
    }

}
