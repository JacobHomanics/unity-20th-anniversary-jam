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
            other.GetComponent<MoveForward>().speed = 0;
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            FindAnyObjectByType<PointsManager>().points += other.GetComponent<EnemyPointRewarder>().amount;
        }
    }
}
