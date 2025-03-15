using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] protected int coins = 100;

    public bool CanAfford(int amount)
    {
        return coins >= amount;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void SpendCoins(int amount)
    {
        if (CanAfford(amount))
        {
            coins -= amount;
        }
        else
        {
            Debug.Log("You don't hace enough money");
        }
    }
}
