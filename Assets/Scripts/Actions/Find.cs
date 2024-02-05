using UnityEngine;
using UnityEngine.AI;
class Find : Action
{
    NavMeshAgent character;
    public Find(NavMeshAgent character)
    {
        this.character = character;
    }
    public override void execute()
    {
        GameObject[] clues = GameObject.FindGameObjectsWithTag("Clue");
        GameObject nearestClue = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject clue in clues) // FIXME; search by closest in navsmesh
        {
            float distance = GetDistanceToTarget(character.transform.position, clue.transform.position);
            if (distance < nearestDistance)
            {
                nearestClue = clue;
                nearestDistance = distance;
            }
        }
        character.SetDestination(nearestClue.transform.position);
        isDone = true;
    }

     public float GetDistanceToTarget(Vector3 originalPosition, Vector3 targetPosition)
        {
            var distance = 0.0f;

            NavMeshPath result = new NavMeshPath();
            var r = character.CalculatePath(targetPosition, result);
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
