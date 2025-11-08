using UnityEngine;
using JacobHomanics.HealthSystem;
using Unity.Cinemachine;

public class BoxDamager : MonoBehaviour
{
    public GameObject audioThing;

    public float damage = 2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Environment"))
        {
            Instantiate(audioThing);
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInChildren<Health>().Current -= damage;
            other.GetComponentInChildren<Animator>().Play("Hit");
            Instantiate(audioThing);
            Destroy(this.gameObject);
        }
    }
}
