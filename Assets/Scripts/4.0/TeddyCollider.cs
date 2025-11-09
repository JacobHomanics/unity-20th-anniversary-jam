using System.Collections;
using UnityEngine;

public class TeddyCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Animator>().Play("Dying");
            other.GetComponent<Collider>().enabled = false;
            FindAnyObjectByType<PointsManager>().points += other.GetComponent<EnemyPointRewarder>().amount;
        }
    }
}
