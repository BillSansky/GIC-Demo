using UnityEngine;
using UnityEngine.Events;

public class CurrencyAsset : ScriptableObject
{
    [SerializeField] private int currencyAmount;

    public UnityEvent OnCurrencyChanged;

    public int CurrencyAmount => currencyAmount;

    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
        OnCurrencyChanged.Invoke();
    }
}