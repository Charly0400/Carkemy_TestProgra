using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Gestiona la visualización y venta de ítems en la tienda
public class ShopItemStore : MonoBehaviour
{
    #region Variables

    [Header("REFERENCES")]
    [SerializeField] protected Image _spriteItem;
    [SerializeField] protected TextMeshProUGUI _nameItem;
    [SerializeField] protected TextMeshProUGUI _priceItem;
    [SerializeField] protected Button _button;

    [SerializeField] protected Item_ScriptableObject _item_SO;

    #endregion

    #region Shop Items Methos

    // Configura ek item de la UI
    public void Setup(Item_ScriptableObject newItem)
    {
        _item_SO = newItem;

        if (_spriteItem != null)
            _spriteItem.sprite = _item_SO.itmeSprite;
        if (_nameItem != null)
            _nameItem.text = _item_SO.itemName;
        if (_priceItem != null)
            _priceItem.text = _item_SO.itemPrice.ToString();

        // Asignar la función del botón de venta
        if (_button != null)
        {
            // Primero, eliminamos cualquier listener previo para evitar duplicados
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnSellButton);
        }  
    }

    //Gestiona la venta del item
    public void OnSellButton()
    {
        Store.Instance.SellItem(_item_SO);
    }

    #endregion
}
