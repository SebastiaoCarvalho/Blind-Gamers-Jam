using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Action currentAction;
    private bool walking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentAction = new Follow(agent); // until I make a behavior tree, the dog will follow the player by default
        walking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAction != null && ! currentAction.IsDone) currentAction.execute();
        if (agent.velocity.magnitude > 0.1f) {
            if (! walking) {
                Debug.Log("Walking");
                Debug.Log(agent.velocity.magnitude + " " + walking);
                walking = true;
                gameObject.GetComponent<StudioEventEmitter>().Play();
            } 
        }
        else {
            if (walking) {
                Debug.Log("Not walking");
                walking = false;
                gameObject.GetComponent<StudioEventEmitter>().Stop();
            }
        }
    }

    public void HearCall() {
        currentAction = new Follow(agent);
    }

    public void FindClue() {
        currentAction = new Find(agent);
    }

}
