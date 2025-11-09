using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed at which the object moves forward")]
    [SerializeField] public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // If no Rigidbody found, log a warning
        if (rb == null)
        {
            Debug.LogWarning("MoveForward: No Rigidbody component found on " + gameObject.name);
        }

        animator = GetComponent<Animator>();
    }

    Animator animator;
    void FixedUpdate()
    {
        animator.SetFloat("Z", 1);
        // Only move if we have a Rigidbody
        if (rb != null)
        {
            // Apply force forward in the object's local forward direction
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
    }
}

