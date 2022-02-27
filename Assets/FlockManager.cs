using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject objectPrefab;

    [SerializeField]
    private int numObjects = 20;
    
    public GameObject[] allObjects;
    
    [SerializeField]
    private Vector3 boxLimits = new Vector3(5, 5, 5);

    [Header("Object Settings")]
    [Range(0.0f, 25.0f)]
    public float minSpeed;
    [Range(0.0f, 25.0f)] 
    public float maxSpeed;
    [Range(0.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)] 
    public float rotationSpeed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        allObjects = new GameObject[numObjects];
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-boxLimits.x, boxLimits.x),
                Random.Range(-boxLimits.y, boxLimits.y),
                Random.Range(-boxLimits.z, boxLimits.z));

            allObjects[i] = (GameObject) Instantiate(objectPrefab, pos, Quaternion.identity);
            allObjects[i].GetComponent<Flock>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
