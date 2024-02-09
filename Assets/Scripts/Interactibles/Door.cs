using UnityEngine;

public class Door : Interactable {

    private bool isOpen = false;
    public override void Interact()
    {
        if (isOpen) gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        else gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        isOpen = ! isOpen;
    }
}