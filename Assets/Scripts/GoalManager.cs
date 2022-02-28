using UnityEngine;

public class GoalManager : MonoBehaviour
{
    /// <summary>
    /// Calls the FlockManager script to move the goal gameobject transform
    /// </summary>
    /// <param name="other">Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<FlockManager>().CollisionDetected(this);
    }
}
