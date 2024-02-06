using UnityEngine;
using UnityEngine.AI;
class Find : Action
{
    private NavMeshAgent character;
    public Find(NavMeshAgent character)
    {
        this.character = character;
    }
    public override void execute() // FIXME: improve to only calculate once or x times instead of runtime
    {
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
        if (nearestClue != null) character.SetDestination(nearestClue.transform.position);
        else Debug.Log("No clues found");
        isDone = true;
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
}
