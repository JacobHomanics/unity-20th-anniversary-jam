using UnityEngine;

public class MeatCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<MeatCounter>().Collect();
            Destroy(this.gameObject);
        }
    }
}
