using UnityEngine;

public class BanaRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 90f; // degrees per second
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // axis to rotate around

    void Update()
    {
        // Rotate continuously
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
