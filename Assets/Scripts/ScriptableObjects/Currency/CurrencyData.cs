using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ScriptableObject/Currency", order = 1)]
public class CurrencyData : ScriptableObject
{
    public string currencyName;
    public Sprite currencyIcon;
    public int startingAmount;
    public int currentAmount;
}
