using UnityEngine;
using UnityEngine.AI;

public class NavMeshRandomPatrol : MonoBehaviour
{
    [Header("Waypoint Settings")]
    [SerializeField] private Transform[] waypoints;

    [Header("Movement Settings")]
    [SerializeField] private float arrivalDistance = 0.5f;
    [SerializeField] private float waitTimeAtWaypoint = 0f;
    [SerializeField] private bool randomizeWaitTime = false;
    [SerializeField] private float minWaitTime = 0f;
    [SerializeField] private float maxWaitTime = 2f;

    [Header("Agent Settings")]
    [SerializeField] private float agentSpeed = 3.5f;
    [SerializeField] private float agentAcceleration = 8f;

    private NavMeshAgent agent;
    private int currentWaypointIndex = -1;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private float currentWaitTime = 0f;

    public Animation anim;

    void Start()
    {
        anim.Play("run");

        // Get or add NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        // Set agent properties
        agent.speed = agentSpeed;
        agent.acceleration = agentAcceleration;

        // Validate waypoints
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("NavMeshRandomPatrol: No waypoints assigned!");
            enabled = false;
            return;
        }

        // Remove any null waypoints
        System.Collections.Generic.List<Transform> validWaypoints = new System.Collections.Generic.List<Transform>();
        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != null)
            {
                validWaypoints.Add(waypoint);
            }
        }

        if (validWaypoints.Count == 0)
        {
            Debug.LogWarning("NavMeshRandomPatrol: No valid waypoints found!");
            enabled = false;
            return;
        }

        waypoints = validWaypoints.ToArray();

        // Start moving to first random waypoint
        SelectRandomWaypoint();
    }

    void Update()
    {
        if (agent == null || waypoints == null || waypoints.Length == 0)
            return;

        // Handle waiting at waypoint
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= currentWaitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                SelectRandomWaypoint();
            }
            return;
        }

        // Check if agent has reached the destination
        if (!agent.pathPending && agent.remainingDistance < arrivalDistance)
        {
            // Start waiting if wait time is set
            if (waitTimeAtWaypoint > 0 || randomizeWaitTime)
            {
                isWaiting = true;
                waitTimer = 0f;

                if (randomizeWaitTime)
                {
                    currentWaitTime = Random.Range(minWaitTime, maxWaitTime);
                }
                else
                {
                    currentWaitTime = waitTimeAtWaypoint;
                }
            }
            else
            {
                // Immediately select new waypoint
                SelectRandomWaypoint();
            }
        }
    }


    public Transform targetWaypoint;

    public void SelectRandomWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        // Select a random waypoint (can be the same one)
        int randomIndex = Random.Range(0, waypoints.Length);

        // Optional: Ensure we don't select the same waypoint (uncomment if desired)
        // while (randomIndex == currentWaypointIndex && waypoints.Length > 1)
        // {
        //     randomIndex = Random.Range(0, waypoints.Length);
        // }

        currentWaypointIndex = randomIndex;
        targetWaypoint = waypoints[currentWaypointIndex];

        if (targetWaypoint != null)
        {
            agent.SetDestination(targetWaypoint.position);
        }
    }

    // Public method to add waypoints at runtime
    public void AddWaypoint(Transform waypoint)
    {
        if (waypoint == null) return;

        System.Collections.Generic.List<Transform> waypointList = new System.Collections.Generic.List<Transform>();
        if (waypoints != null)
        {
            waypointList.AddRange(waypoints);
        }
        waypointList.Add(waypoint);
        waypoints = waypointList.ToArray();
    }

    public void RemoveWaypoint(Transform waypoint)
    {
        if (waypoint == null) return;

        System.Collections.Generic.List<Transform> waypointList = new System.Collections.Generic.List<Transform>();

        waypointList.Remove(waypoint);
        waypoints = waypointList.ToArray();

        Debug.Log(targetWaypoint);
        if (targetWaypoint)
        {
            Debug.Log("New Waypoint");
            SelectRandomWaypoint();
        }
    }

    // Public method to set waypoints at runtime
    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        if (agent != null && waypoints != null && waypoints.Length > 0)
        {
            SelectRandomWaypoint();
        }
    }

    // Public method to update agent speed at runtime
    public void SetSpeed(float speed)
    {
        agentSpeed = speed;
        if (agent != null)
        {
            agent.speed = agentSpeed;
        }
    }
}

