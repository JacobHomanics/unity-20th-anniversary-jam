using UnityEngine;

public class BananaShooter : MonoBehaviour
{
    [Header("Banana Settings")]
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Speed Settings")]
    [SerializeField] private float minShootForce = 8f;
    [SerializeField] private float maxShootForce = 15f;

    [Header("Size Settings")]
    [SerializeField] private float minScale = 0.7f;
    [SerializeField] private float maxScale = 1.3f;

    [Header("Shooting Intervals")]
    [SerializeField] private float minShootInterval = 1f;
    [SerializeField] private float maxShootInterval = 3f;

    private float timeElapsed = 0f;
    private float currentShootInterval = 0f;

    void Start()
    {
        // Set initial random shoot interval
        currentShootInterval = Random.Range(minShootInterval, maxShootInterval);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Check if enough time has passed to shoot
        if (timeElapsed >= currentShootInterval)
        {
            ShootBanana();

            // Reset timer and set new random interval
            timeElapsed = 0f;
            currentShootInterval = Random.Range(minShootInterval, maxShootInterval);
        }
    }

    void ShootBanana()
    {
        if (bananaPrefab == null)
        {
            Debug.LogWarning("Banana prefab is not assigned in BananaShooter!");
            return;
        }

        // Determine spawn position and rotation
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRotation = firePoint != null ? firePoint.rotation : transform.rotation;

        // If no fire point, shoot forward in the direction the object is facing
        if (firePoint == null)
        {
            spawnRotation = transform.rotation;
        }

        // Instantiate the banana
        GameObject banana = Instantiate(bananaPrefab, spawnPosition, spawnRotation);

        // Randomize banana size
        float randomScale = Random.Range(minScale, maxScale);
        banana.transform.localScale = Vector3.one * randomScale;

        // Add force to the banana if it has a Rigidbody
        Rigidbody bananaRb = banana.GetComponent<Rigidbody>();
        if (bananaRb != null)
        {
            // Randomize shoot speed
            float randomSpeed = Random.Range(minShootForce, maxShootForce);
            bananaRb.AddForce(spawnRotation * Vector3.forward * randomSpeed, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning("Banana prefab doesn't have a Rigidbody component! Banana won't move.");
        }
    }
}
