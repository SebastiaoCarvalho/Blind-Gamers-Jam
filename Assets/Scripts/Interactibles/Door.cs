using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

    private bool isOpen = false;
    public List<Lock> locks = new List<Lock>();
    public override void Interact()
    {
        if (locks.Count > 0) Debug.Log("Door is locked");
        if (isOpen) gameObject.GetComponent<Rigidbody>().detectCollisions = false;
        else gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        isOpen = ! isOpen;
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