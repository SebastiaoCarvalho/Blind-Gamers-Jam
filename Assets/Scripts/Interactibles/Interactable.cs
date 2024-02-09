using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact() { // override ub««in children classes
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(player.GetComponent<Player>());
        player.GetComponent<Player>().AddInteractible(gameObject);
    }

    private void OnTriggerExit(Collider other) {
        player.GetComponent<Player>().RemoveInteractible(gameObject);
    }
}
