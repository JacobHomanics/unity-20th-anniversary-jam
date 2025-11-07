using UnityEngine;
using JacobHomanics.HealthSystem;
using Unity.Cinemachine;

public class BoxDamager : MonoBehaviour
{
    public float damage = 2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInChildren<Health>().Current -= damage;
            other.GetComponentInChildren<Animator>().Play("Hit");
            Destroy(this.gameObject);
        }
    }
}
