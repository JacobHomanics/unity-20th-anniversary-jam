using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float rotationSpeed = 30f; // degrees per second
    [SerializeField] private float radius = 5f;
    [SerializeField] private bool randomizeRadius = true;
    [SerializeField] private float minRadius = 3f;
    [SerializeField] private float maxRadius = 8f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // axis to rotate around

    [Header("Radius Randomization Timing")]
    [SerializeField] private float minTimeInterval = 2f;
    [SerializeField] private float maxTimeInterval = 5f;
    [SerializeField] private float radiusLerpSpeed = 2f; // speed of radius interpolation

    [Header("Axis Randomization")]
    [SerializeField] private bool randomizeAxis = true;
    [SerializeField] private float minAxisTimeInterval = 3f;
    [SerializeField] private float maxAxisTimeInterval = 7f;

    private float currentAngle = 0f;
    private float timeElapsed = 0f;
    private float currentTimeInterval = 0f;
    private float targetRadius = 5f;
    private float axisTimeElapsed = 0f;
    private float currentAxisTimeInterval = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (centerPoint == null)
        {
            Debug.LogWarning("CenterPoint Transform is not assigned in MonkeyController!");
            return;
        }

        // Randomize radius if enabled
        if (randomizeRadius)
        {
            targetRadius = Random.Range(minRadius, maxRadius);
            radius = targetRadius; // Initialize radius to target
            // Set initial random time interval
            currentTimeInterval = Random.Range(minTimeInterval, maxTimeInterval);
        }
        else
        {
            targetRadius = radius;
        }

        // Randomize axis if enabled
        if (randomizeAxis)
        {
            rotationAxis = Random.Range(0, 2) == 0 ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0);
            // Set initial random time interval for axis
            currentAxisTimeInterval = Random.Range(minAxisTimeInterval, maxAxisTimeInterval);
        }

        // Calculate initial angle based on current position
        Vector3 directionToCenter = transform.position - centerPoint.position;
        if (directionToCenter.magnitude > 0.01f)
        {
            currentAngle = Mathf.Atan2(directionToCenter.x, directionToCenter.z) * Mathf.Rad2Deg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (centerPoint == null) return;

        // Handle radius randomization timing
        if (randomizeRadius)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= currentTimeInterval)
            {
                // Set new target radius (will lerp to this)
                targetRadius = Random.Range(minRadius, maxRadius);

                // Reset timer and set new random time interval
                timeElapsed = 0f;
                currentTimeInterval = Random.Range(minTimeInterval, maxTimeInterval);
            }

            // Lerp radius towards target radius
            radius = Mathf.Lerp(radius, targetRadius, radiusLerpSpeed * Time.deltaTime);
        }

        // Handle axis randomization timing
        if (randomizeAxis)
        {
            axisTimeElapsed += Time.deltaTime;

            if (axisTimeElapsed >= currentAxisTimeInterval)
            {
                // Randomize the rotation axis between Vector3(0,1,0) and Vector3(0,-1,0)
                rotationAxis = Random.Range(0, 2) == 0 ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0);

                // Reset timer and set new random time interval
                axisTimeElapsed = 0f;
                currentAxisTimeInterval = Random.Range(minAxisTimeInterval, maxAxisTimeInterval);
            }
        }

        // Update the angle based on rotation speed and axis direction
        // When axis is (0,-1,0), rotate in opposite direction
        float effectiveRotationSpeed = rotationSpeed * rotationAxis.y;
        currentAngle += effectiveRotationSpeed * Time.deltaTime;

        // Calculate new position using trigonometry
        float angleInRadians = currentAngle * Mathf.Deg2Rad;

        // Rotate around the center point
        // The rotation direction is already handled by effectiveRotationSpeed
        Vector3 offset = new Vector3(
            Mathf.Sin(angleInRadians) * radius,
            0f,
            Mathf.Cos(angleInRadians) * radius
        );

        transform.position = centerPoint.position + offset;

        // Optionally make the object face the center point
        transform.LookAt(centerPoint.position);
    }
}
