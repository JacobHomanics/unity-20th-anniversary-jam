using System.Collections;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public int points;

    public TMP_Text text;

    public int threshold;


    void Update()
    {
        text.text = points.ToString();

        if (points >= threshold)
        {
            Debug.Log("Won game");
        }
    }
}
