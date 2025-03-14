using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinStore : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _priceItem;
    [SerializeField] protected TextMeshProUGUI _nameItem;
    [SerializeField] protected Image _spriteItem;
    [SerializeField] protected Button _button;

    [SerializeField] protected Skin_ScriptableObject _skin_SO;

    public void Setup(Skin_ScriptableObject newItem)
    {
        _skin_SO = newItem;

        if (_spriteItem != null)
            _spriteItem.sprite = _skin_SO.skinSprite;
        if (_nameItem != null)
            _nameItem.text = _skin_SO.skinName;
        if (_priceItem != null)
            _priceItem.text = _skin_SO.skinPrice.ToString();

        // Asignar la función del botón de venta
        if (_button != null)
        {
            // Primero, eliminamos cualquier listener previo para evitar duplicados
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnBuyButton);
        }
    }
    public void OnBuyButton()
    {
        Store.Instance.BuyItem(_skin_SO);
    }

    public Skin_ScriptableObject GetSkisSO
    {
        get { return _skin_SO; }
    }
}
