using UnityEngine;
using UnityEngine.InputSystem;
using JacobHomanics.TimerSystem;

public class BoxShooter : MonoBehaviour
{
    [Header("Box Settings")]
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Speed Settings")]
    [SerializeField] private float minShootForce = 8f;
    [SerializeField] private float maxShootForce = 15f;

    [Header("Size Settings")]
    [SerializeField] private float minScale = 0.7f;
    [SerializeField] private float maxScale = 1.3f;

    public Timer timer;

    // [Header("Input Settings")]
    // [SerializeField] private InputActionAsset inputActions;
    // private InputAction attackAction;

    // void Awake()
    // {
    //     // Get the Attack action from the input actions
    //     InputActionMap playerMap = inputActions.FindActionMap("Player");
    //     attackAction = playerMap.FindAction("Attack");
    // }

    // void OnEnable()
    // {
    //     attackAction.Enable();
    //     attackAction.performed += OnAttackPerformed;
    // }

    // void OnDisable()
    // {
    //     // Unsubscribe and disable the attack action
    //     if (attackAction != null)
    //     {
    //         attackAction.performed -= OnAttackPerformed;
    //         attackAction.Disable();
    //     }
    // }

    // void OnAttackPerformed(InputAction.CallbackContext context)
    // {
    //     Debug.Log("OnAttackPerformed");
    //     ShootBox();
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && timer.IsDurationReached())
        {
            ShootBox();
            timer.Restart();
        }
    }

    void ShootBox()
    {
        // Determine spawn position and rotation
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRotation = firePoint != null ? firePoint.rotation : transform.rotation;


        // Instantiate the box
        GameObject box = Instantiate(boxPrefab, spawnPosition, spawnRotation);

        // Randomize box size
        float randomScale = Random.Range(minScale, maxScale);
        box.transform.localScale = Vector3.one * randomScale;

        // Add force to the box if it has a Rigidbody
        Rigidbody boxRb = box.GetComponent<Rigidbody>();
        // Randomize shoot speed
        float randomSpeed = Random.Range(minShootForce, maxShootForce);
        boxRb.AddForce(spawnRotation * Vector3.forward * randomSpeed, ForceMode.VelocityChange);
    }
}

