using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PointsManager : MonoBehaviour
{
    public int points;

    public TMP_Text text;

    public int threshold;

    public UnityEvent onThreshold;

    void Update()
    {
        text.text = points.ToString();

        if (points >= threshold)
        {
            Debug.Log("Won game");
            onThreshold?.Invoke();
        }
    }
}
