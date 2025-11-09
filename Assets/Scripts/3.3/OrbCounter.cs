using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OrbCounter : MonoBehaviour
{
    public UnityEvent onCount;

    public int orbCounter;
    public TMP_Text text;

    void Update()
    {
        text.text = orbCounter.ToString();

        if (orbCounter >= 5)
        {
            onCount?.Invoke();
        }
    }
}
