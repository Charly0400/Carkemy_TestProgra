using UnityEngine;
using TMPro;

// Gestiona la moneda del jugador y su visualización en la UI.

public class CurrencyManager : MonoBehaviour
{
    #region Variables

    [Header("COINS")]
    [SerializeField] protected int coins = 100;

    [Header("REFERENCES FOR TEXT COIN")]
    [SerializeField] protected TextMeshProUGUI _moneyText;

    #endregion

    #region Unity Methods
    //Se actualiza el total de las monedas del jugador
    private void Update()
    {
        _moneyText.text = coins.ToString();    
    }

    #endregion

    #region Buying and Selling System

    //Verifica si el jugador puede adquirir el producto
    public bool CanAfford(int amount)
    {
        return coins >= amount;
    }

    //Añade monedas al jugador
    public void AddCoins(int amount)
    {
        coins += amount;
    }

    //Resta monedas solo sí el jugador tiene las suficientes monedas
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

    #endregion
}
