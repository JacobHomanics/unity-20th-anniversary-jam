using UnityEngine;
using JacobHomanics.TimerSystem;

public class TimerRandomizer : MonoBehaviour
{
    public Timer timer;

    public float min;
    public float max;

    public void Randomize()
    {
        timer.Duration = Random.Range(min, max);
    }
}
