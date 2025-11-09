using TMPro;
using UnityEngine;

public class OrbCounter : MonoBehaviour
{
    public int orbCounter;
    public TMP_Text text;

    void Update()
    {
        text.text = orbCounter.ToString();
    }
}
