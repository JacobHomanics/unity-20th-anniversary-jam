using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MoveForward moveForward;
    public GameObject[] gos;

    [SerializeField] private float minShootForce = 8f;
    [SerializeField] private float maxShootForce = 15f;

    public void Spawn()
    {
        var rn = Random.Range(0, gos.Length);
        var box = Instantiate(gos[rn], transform.position, transform.rotation);

        box.GetComponent<MoveForward>().speed = Random.Range(minShootForce, maxShootForce);
    }
}
