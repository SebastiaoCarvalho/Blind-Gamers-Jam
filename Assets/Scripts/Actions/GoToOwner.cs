using UnityEngine;
using UnityEngine.AI;

class GoToOwner : Action
{

    NavMeshAgent character;
    GameObject owner;
    private float radius = 5.0f;

    public GoToOwner(NavMeshAgent character)
    {
        this.character = character;
        this.owner = GameObject.Find("Player");
    }

    public override void execute()
    {
        character.SetDestination(owner.transform.position);
        if (Vector3.Distance(character.transform.position, owner.transform.position) < radius)
        {
            isDone = true;
        }
    }

}
