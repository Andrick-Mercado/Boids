using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlockManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] 
    private GameObject objectPrefab;

    [Header("Object Settings")]
    [Range(0.0f, 25.0f)]
    public float minSpeed;
    [Range(0.0f, 25.0f)] 
    public float maxSpeed;
    [Range(0.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)] 
    public float rotationSpeed;
    
    [Header("Limits")]
    [SerializeField]
    private int numObjects = 20;
    
    public GameObject[] allObjects;
    
    [SerializeField]
    private Vector3 boxLimitsStart = new Vector3(5, 5, 5);
    
    [SerializeField]
    private Vector3 boxLimit = new Vector3(100, 100, 100);
    
    //[HideInInspector]
    public Vector3 goalPos;
    
    private void Start()
    {
        allObjects = new GameObject[numObjects];
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-boxLimitsStart.x, boxLimitsStart.x),
                Random.Range(-boxLimitsStart.y, boxLimitsStart.y),
                Random.Range(-boxLimitsStart.z, boxLimitsStart.z));

            allObjects[i] = (GameObject) Instantiate(objectPrefab, pos, Quaternion.identity);
            allObjects[i].GetComponent<Flock>().myManager = this;
        }

        goalPos = this.transform.position;
    }

    private void Update()
    {
        goalPos = this.transform.position + new Vector3(Random.Range(-boxLimit.x, boxLimit.x),
            Random.Range(-boxLimit.y, boxLimit.y),
            Random.Range(-boxLimit.z, boxLimit.z));
    }
}
