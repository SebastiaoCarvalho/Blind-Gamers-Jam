using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Door : Interactable {

    private bool isOpen = false;
    public List<Lock> locks = new List<Lock>();
    public override void Interact()
    {
        if (locks.Count > 0) {
            Debug.Log("Door is locked");
            StudioEventEmitter sound = gameObject.GetComponent<StudioEventEmitter>();
            sound.EventReference = FMODUnity.RuntimeManager.PathToEventReference("event:/Sound Effects/Locked Door/Door_Locked");
            sound.Play();
            return;
        }
        if (! isOpen) { // open door
            gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            StudioEventEmitter sound = gameObject.GetComponent<StudioEventEmitter>();
            sound.EventReference = FMODUnity.RuntimeManager.PathToEventReference("event:/Sound Effects/Locked Door/Door_Open");
            sound.Play();
            gameObject.tag = "Untagged";
        }
        isOpen = true;
    }

    public bool UseKey(string key) {
        foreach (Lock l in locks) {
            bool open = l.Unlock(key);
            if (open) return true;
        }
        return false;
    }

    public void AddLock(Lock lockk) {
        locks.Add(lockk);
    }

    public void OpenLock(Lock lockk) {
        locks.Remove(lockk);
    }
}