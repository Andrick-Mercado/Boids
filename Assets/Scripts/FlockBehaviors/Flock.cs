using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;

    private float _speed;
    
    void Start()
    {
        _speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }
    
    void Update()
    {
        _speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * _speed);
    }

    /// <summary>
    /// Uses alingment, cohesion, and separation to provide flocking behavior
    /// </summary>
    void ApplyRules()
    {
        GameObject[] gos;
        gos = myManager.allObjects;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= myManager.neighbourDistance)
                {
                    vcentre = go.transform.position;
                    groupSize++;

                    if (nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock._speed;
                }
            }
            
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (myManager.goalTransform.position * 2 - this.transform.position);
            _speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction),
                    myManager.rotationSpeed * Time.deltaTime);
        }
    }
}
