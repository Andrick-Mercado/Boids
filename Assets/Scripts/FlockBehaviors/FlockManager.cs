using UnityEngine;
using Random = UnityEngine.Random;

public class FlockManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] 
    private GameObject objectPrefab;
    [SerializeField] 
    private GameObject[] checkpoints;

    [Header("Object Settings")]
    [Range(0.0f, 25.0f)]
    public float minSpeed;
    [Range(0.0f, 25.0f)] 
    public float maxSpeed;
    [Range(0.0f, 25.0f)]
    public float neighbourDistance;
    [Range(0.0f, 25.0f)] 
    public float rotationSpeed;

    [Header("Limits")] [SerializeField] private int numObjects = 20;
    public GameObject[] allObjects;
    [SerializeField]
    private Vector3 boxLimitsStart = new Vector3(5, 5, 5);
    [SerializeField]
    private Vector3 boxLimit = new Vector3(100, 100, 100);
    public Transform goalTransform;
    
    [Header("Algorithm")]
    [SerializeField]
    private MyEnum selectAlgorithm = new MyEnum();
    
    //INSIDE CLASS VARIABLES
    private bool _isRandomAlgorithm;
    private int _curIndex;
    public enum MyEnum
    {
        LazyFlight,
        CircleTree
    };

    private void Awake()
    {
        _isRandomAlgorithm = selectAlgorithm.ToString().Equals("LazyFlight");
        _curIndex = 1;
    }

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
        
        goalTransform.position =
            _isRandomAlgorithm ? this.transform.position + new Vector3(Random.Range(-boxLimit.x, boxLimit.x),
                Random.Range(-boxLimit.y, boxLimit.y),
                Random.Range(-boxLimit.z, boxLimit.z)) : checkpoints[0].transform.position;
    }
    
    /// <summary>
    /// moves the goal transform according to the algorithm applied at the start
    /// cannot be changed once project is run
    /// </summary>
    /// <param name="goalManager">brings the origin script</param>
    public void CollisionDetected(GoalManager goalManager)
    {
        if (_isRandomAlgorithm)
        {
            goalTransform.position = this.transform.position + new Vector3(Random.Range(-boxLimit.x, boxLimit.x),
                Random.Range(-boxLimit.y, boxLimit.y),
                Random.Range(-boxLimit.z, boxLimit.z));
        }
        else if(checkpoints.Length > _curIndex)
        {
            goalTransform.position = checkpoints[_curIndex].transform.position;
            _curIndex++;
        }
    }
}
