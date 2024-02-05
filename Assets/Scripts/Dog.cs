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
    }

    // Update is called once per frame
    void Update()
    {
        currentAction?.execute();
    }

    public void HearCall() {
        currentAction = new GoToOwner(agent);
    }

    public void FindClue() {
        currentAction = new Find(agent);
    }

}
