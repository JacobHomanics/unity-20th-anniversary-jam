using TMPro;
using UnityEngine;

public class MeatCounter : MonoBehaviour
{
    public TMP_Text text;

    [SerializeField] private int totalAmount;

    public void Collect()
    {
        totalAmount -= 1;
        if (totalAmount <= 0)
        {
            Debug.Log("End Scene");
        }
    }

    void Update()
    {
        text.text = totalAmount.ToString();
    }
}
