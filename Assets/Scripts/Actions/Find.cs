using UnityEngine;
class Find : Action
{
    GameObject character;
    public Find(GameObject character)
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
            float distance = Vector3.Distance(clue.transform.position, character.transform.position);
            if (distance < nearestDistance)
            {
                nearestClue = clue;
                nearestDistance = distance;
            }
        }
        isDone = true;
    }
}
