using JacobHomanics.TimerSystem;
using UnityEngine;

public class OrbCollector : MonoBehaviour
{
    public AudioClip[] playerAs;
    public AudioClip[] enemyAs;

    public AudioSource audioSource;
    public float timeReduction = 5f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var rn = Random.Range(0, playerAs.Length);
            audioSource.clip = playerAs[rn];
            audioSource.Play();

            var nrp = FindAnyObjectByType<NavMeshRandomPatrol>();
            if (nrp.targetWaypoint == this.transform)
            {
                nrp.SelectRandomWaypoint();
            }

            FindAnyObjectByType<OrbCounter>().orbCounter++;
            if (FindAnyObjectByType<OrbCounter>().orbCounter >= 5)
            {
                Debug.Log("Scene won!");
            }

            Destroy(this.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            var rn = Random.Range(0, enemyAs.Length);
            audioSource.clip = enemyAs[rn];
            audioSource.Play();
            FindAnyObjectByType<Timer>().ElapsedTime += timeReduction;
            Destroy(this.gameObject);
        }


    }
}
