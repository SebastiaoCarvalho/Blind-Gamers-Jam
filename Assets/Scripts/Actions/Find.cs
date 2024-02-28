using FMODUnity;
using UnityEngine;
using UnityEngine.AI;
class Find : Action
{
    private NavMeshAgent character;
    private GameObject target;
    public Find(NavMeshAgent character)
    {
        this.character = character;
    }
    public override void execute() // FIXME: improve to only calculate once or x times instead of runtime
    {
        if (target == null) {
            target = GetTarget();
            character.SetDestination(target.transform.position);
            character.stoppingDistance = 1.5f;
        }
        if (target != null && HasArrived()) {
            character.GetComponent<Dog>().Bark();
            isDone = true;
            Debug.Log("Arrived at clue");
        }
        else if (target == null) {
            isDone = true;
            Debug.Log("No clues found");
        }
    }

    private GameObject GetTarget() {
        GameObject[] clues = GameObject.FindGameObjectsWithTag("Clue");
        Debug.Log(clues.Length + " clues found");
        GameObject nearestClue = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject clue in clues)
        {
            var tmpPosition = character.transform.position;
            tmpPosition.y = character.transform.position.y;

            float distance = GetDistanceToTarget(character.transform.position, tmpPosition);

            if (distance < nearestDistance)
            {
                nearestClue = clue;
                nearestDistance = distance;
            }
        }
        return nearestClue;
    }

    public float GetDistanceToTarget(Vector3 originalPosition, Vector3 targetPosition)
    {
        var distance = 0.0f;

        NavMeshPath result = new NavMeshPath();
        var r = character.CalculatePath(targetPosition, result);
        if (!r) Debug.Log("No path found!");
        if (r == true)
        {
            var currentPosition = originalPosition;
            foreach (var c in result.corners)
            {
                //Rough estimate, it does not account for shortcuts so we have to multiply it
                distance += Vector3.Distance(currentPosition, c) * 0.65f;
                currentPosition = c;
            }
            return distance;
        }
        //Default value
        return Mathf.Infinity;
    }

    private bool HasArrived() {
        if (!character.pathPending)
        {
            if (character.remainingDistance <= character.stoppingDistance)
            {
                if (!character.hasPath || character.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
