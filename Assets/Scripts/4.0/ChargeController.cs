using UnityEngine;
using JacobHomanics.Essentials.RPGController;
using JacobHomanics.TimerSystem;

public class ChargeController : MonoBehaviour
{
    [Header("Input Settings")]
    [Tooltip("The button name from the Input Manager (e.g., 'Fire1', 'Fire2', 'Jump')")]
    public string buttonName = "Fire1";

    [Header("Charge Settings")]
    [Tooltip("Maximum charge value")]
    public float maxCharge = 10f;

    // [Tooltip("Time in seconds to reach maximum charge")]
    // public float chargeDuration = 2f;


    [Header("Debug")]
    [Tooltip("Current charge value (0 to maxCharge)")]
    [SerializeField] private float currentCharge = 0f;

    // Public property to access current charge
    public float CurrentCharge => currentCharge;

    // Normalized charge (0 to 1)
    public float NormalizedCharge => currentCharge / maxCharge;

    // Last charge value when button was released (before reset)
    public float LastReleasedCharge { get; private set; } = 0f;
    public float LastReleasedNormalizedCharge => LastReleasedCharge / maxCharge;

    // Events for when charge reaches max or is released
    public System.Action OnChargeComplete;
    public System.Action<float> OnChargeReleased;

    private bool wasCharging = false;

    public PlayerMotor playerMotor;

    public Timer timer;
    public GameObject timerUI;

    bool canCharge = true;

    void Start()
    {
        playerMotor.events.otherEvents.Grounded.AddListener(() => { canCharge = true; });
    }

    void Update()
    {
        if (!canCharge)
        {
            return;
        }
        bool isHoldingButton = Input.GetKey(KeyCode.Space);
        bool isButtonUp = Input.GetKeyUp(KeyCode.Space);

        if (isButtonUp)
        {
            // Store the charge value before resetting
            LastReleasedCharge = currentCharge;
            OnChargeReleased?.Invoke(LastReleasedCharge);
            canCharge = false;
            timer.Restart();
            currentCharge = 0;
        }



        timer.enabled = isHoldingButton;
        timerUI.SetActive(isHoldingButton);


        if (isHoldingButton)
        {
            // Increase charge while button is held (based on duration)
            float chargeRate = maxCharge / timer.Duration;
            currentCharge += chargeRate * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);

            // Check if we just reached max charge
            if (currentCharge >= maxCharge && !wasCharging)
            {
                OnChargeComplete?.Invoke();
            }

            wasCharging = true;
        }

        playerMotor.modifiableMotorValues.jumpPower = currentCharge;
    }

    // Method to manually reset charge
    public void ResetCharge()
    {
        currentCharge = 0f;
    }

    // Method to set charge to a specific value
    public void SetCharge(float value)
    {
        currentCharge = Mathf.Clamp(value, 0f, maxCharge);
    }
}

