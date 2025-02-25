using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Currency", order = 1)]
public class CurrencyData : ScriptableObject
{
    public string currencyName;
    public Sprite currencyIcon;
    public int startingAmount;
    public int currentAmount;

    [SerializeField] public UnityAction<int> onCurrencyChanged;

    public void Add(int value)
    {
        currentAmount += value; // Increase the stored score
        onCurrencyChanged?.Invoke(currentAmount); // Notify listeners
    }
    public void Subtract(int value)
    {
        currentAmount -= value; // Increase the stored score
        onCurrencyChanged?.Invoke(currentAmount); // Notify listeners
    }

    public int GetCurrency()
    {
        return currentAmount;
    }

    public void Subscribe(UnityAction<int> function)
    {
        onCurrencyChanged += function;
    }

    public void UnSubscribe(UnityAction<int> function)
    {
        onCurrencyChanged -= function;
    }
}
