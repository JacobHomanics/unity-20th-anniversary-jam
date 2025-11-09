using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MeatCounter : MonoBehaviour
{
    public TMP_Text text;

    [SerializeField] private int totalAmount;

    public UnityEvent onZero;

    public void Collect()
    {
        totalAmount -= 1;
        if (totalAmount <= 0)
        {
            onZero?.Invoke();
        }
    }

    void Update()
    {
        text.text = totalAmount.ToString();
    }
}
