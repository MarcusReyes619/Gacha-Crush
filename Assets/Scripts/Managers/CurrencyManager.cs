using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public CurrencyData currency;

    private void Awake()
    {
        // Singleton Pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager
        }

        LoadNewCurrency();
    }

    private void LoadNewCurrency()
    {
        //Load saved currency amount or use starting amount
        currency.currentAmount = PlayerPrefs.GetInt(currency.currencyName, currency.startingAmount);
    }

    public int GetCurrency()
    {
        return currency.currentAmount;
    }

    public void AddCurrency(int amount)
    {
        currency.currentAmount += amount;
        SaveCurrency();
    }

    public bool SpendCurrency(int amount)
    {
        if (currency.currentAmount >= amount)
        {
            currency.currentAmount -= amount;
            SaveCurrency();
            return true;
        }
        return false;
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetInt(currency.currencyName, currency.currentAmount);
        PlayerPrefs.Save();
    }
}
