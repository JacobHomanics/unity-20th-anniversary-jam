using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;

    [Header("Rotation Settings")]
    [SerializeField] private bool smoothRotation = false;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Axis Settings")]
    [SerializeField] private bool lockXAxis = false;
    [SerializeField] private bool lockYAxis = false;
    [SerializeField] private bool lockZAxis = false;

    private Transform targetTransform;

    void Start()
    {
        targetTransform = target;

    }

    void Update()
    {
        Vector3 targetPosition = targetTransform.position;
        Vector3 direction = targetPosition - transform.position;

        // Lock axes if specified
        if (lockXAxis) direction.x = 0;
        if (lockYAxis) direction.y = 0;
        if (lockZAxis) direction.z = 0;

        // Only rotate if direction is not zero
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            if (smoothRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = targetRotation;
            }
        }
    }

    // Public method to set target at runtime
    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }
}

