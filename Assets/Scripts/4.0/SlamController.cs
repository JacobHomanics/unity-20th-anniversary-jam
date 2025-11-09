using System.Collections;
using JacobHomanics.Essentials.RPGController;
using TMPro;
using UnityEngine;

public class SlamController : MonoBehaviour
{
    [SerializeField] private float slamRadius = 5f;
    [SerializeField] private float maxSlamRadius = 15f;
    [SerializeField] private float visualDuration = 0.5f;
    [SerializeField] private Material slamVisualMaterial;
    [SerializeField] private ChargeController chargeController;

    public PlayerMotor playerMotor;

    void Start()
    {
        StartCoroutine(Yee());
    }

    IEnumerator Yee()
    {
        yield return new WaitForSeconds(2f);
        playerMotor.events.otherEvents.Grounded.AddListener(Slam);

    }


    public void Slam()
    {
        // Calculate effective radius based on charge
        float effectiveRadius = slamRadius;
        if (chargeController != null)
        {
            // Use the last released charge value (when button was released)
            float normalizedCharge = chargeController.LastReleasedNormalizedCharge;
            effectiveRadius = Mathf.Lerp(slamRadius, maxSlamRadius, normalizedCharge);
        }

        // Cast a sphere to detect enemies
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, effectiveRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<Animator>().Play("Dying");
                FindAnyObjectByType<PointsManager>().points += hitCollider.GetComponent<EnemyPointRewarder>().amount;
            }
        }

        // Create visual representation of the slam radius
        CreateSlamVisual(effectiveRadius);
    }

    public GameObject spherePrefab;

    private void CreateSlamVisual(float radius)
    {
        // Create a sphere GameObject to visualize the slam radius
        // GameObject visualSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var visualSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        visualSphere.name = "SlamVisual";
        visualSphere.transform.position = transform.position;
        // Make it flat - wide on X and Z, thin on Y
        visualSphere.transform.localScale = new Vector3(radius * 2f, 0.05f, radius * 2f);

        // Remove the collider (we only want visual)
        // Destroy(visualSphere.GetComponent<Collider>());

        // Apply material if provided, otherwise use default
        MeshRenderer renderer = visualSphere.GetComponent<MeshRenderer>();
        if (slamVisualMaterial != null)
        {
            renderer.material = slamVisualMaterial;
        }
        else
        {
            // Create a simple transparent material
            Material mat = new Material(Shader.Find("Standard"));
            mat.SetFloat("_Mode", 3); // Transparent mode
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            mat.color = new Color(1f, 0f, 0f, 0.3f); // Red with transparency
            renderer.material = mat;
        }

        // Destroy the visual after the duration
        Destroy(visualSphere, visualDuration);
    }
}
