using UnityEngine;
using UnityEngine.AI;

class Follow : Action {

    private NavMeshAgent character;
    private GameObject target;
    private float radius = 5.0f;

    public Follow(NavMeshAgent character) {
        this.character = character;
        this.target = GameObject.Find("Player");
    }

    public override void execute() { // This action is never done, it will keep following the target until interrupted
        character.SetDestination(target.transform.position);
        if (Vector3.Distance(character.transform.position, target.transform.position) < radius) {
            character.ResetPath();
        }
    }
}