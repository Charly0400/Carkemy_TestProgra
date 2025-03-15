using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Gestiona la visualización y comparade de skins
public class ShopSkinStore : MonoBehaviour
{
    #region Variable

    [Header("REFERENCES")]
    [SerializeField] protected TextMeshProUGUI _priceItem;
    [SerializeField] protected TextMeshProUGUI _nameItem;
    [SerializeField] protected Image _spriteItem;
    [SerializeField] protected Button _button;
    [SerializeField] protected Button _buttonEquip;
    [SerializeField] protected Skin_ScriptableObject _skin_SO;

    #endregion

    #region Shop Skins Methods
    //Configuración de la tienda
    public void Setup(Skin_ScriptableObject newItem)
    {
        _skin_SO = newItem;
        // Asigna el sprite, nombre y precio de la skin a los componentes de la UI.
        if (_spriteItem != null)
            _spriteItem.sprite = _skin_SO.skinSprite;
        if (_nameItem != null)
            _nameItem.text = _skin_SO.skinName;
        if (_priceItem != null)
            _priceItem.text = _skin_SO.skinPrice.ToString();
      
        if (_button != null)
        {           
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(OnBuyButton);
        }

        if (_buttonEquip != null)
        {
            _buttonEquip.onClick.RemoveAllListeners();
            _buttonEquip.onClick.AddListener(OnEquipButton);
            _buttonEquip.gameObject.SetActive(false); 
        }
    }

    public void OnBuyButton()
    {
        Store.Instance.BuyItem(_skin_SO, this);
    }

    public void OnEquipButton()
    {
        Skin_Manager.Instance.EquipSkin(_skin_SO);
    }

    #endregion

    #region GETTERS & SETTERS

    public Skin_ScriptableObject GetSkisSO
    {
        get { return _skin_SO; }
    }

    public Button GetPriceButton
    {
        get { return _button; }
        set { _button = value; }
    }
    public Button GetEquipButton
    {
        get { return _buttonEquip; }
        set { _buttonEquip = value; }
    }

    #endregion
}