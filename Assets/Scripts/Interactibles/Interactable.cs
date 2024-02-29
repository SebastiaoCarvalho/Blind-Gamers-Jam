using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact() { // override in children classes
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 0) return; // Bugs with some default layer objects
        Debug.Log(player.GetComponent<Player>());
        Debug.Log(other.gameObject.layer);
        player.GetComponent<Player>().AddInteractible(gameObject);
    }

    private void OnTriggerExit(Collider other) {
        player.GetComponent<Player>().RemoveInteractible(gameObject);
    }
}
