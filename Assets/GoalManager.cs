using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<FlockManager>().CollisionDetected(this);
    }
}
