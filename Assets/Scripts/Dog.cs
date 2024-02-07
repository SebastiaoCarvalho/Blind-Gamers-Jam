using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Action currentAction;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentAction = new Follow(agent); // until I make a behavior tree, the dog will follow the player by default
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAction != null && ! currentAction.IsDone) currentAction.execute();
    }

    public void HearCall() {
        currentAction = new Follow(agent);
    }

    public void FindClue() {
        currentAction = new Find(agent);
    }

}
