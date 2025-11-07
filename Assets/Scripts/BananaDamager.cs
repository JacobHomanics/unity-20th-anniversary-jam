using UnityEngine;
using JacobHomanics.HealthSystem;
using Unity.Cinemachine;

public class BanaDamager : MonoBehaviour
{
    public float damage = 2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Health>().Current -= damage;
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            Destroy(this.gameObject);
        }
    }
}
