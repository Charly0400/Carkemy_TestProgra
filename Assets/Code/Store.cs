using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    #region Variables

    [Header ("REFERENCES")]
    [SerializeField] protected CurrencyManager _currencyManager;
    [SerializeField] protected GameManager _gameManager;

    [Header ("INVENTORY ITEMS")]    
    [SerializeField] protected List<Item_ScriptableObject> _items_SO = new List<Item_ScriptableObject>();
    
    [Header ("INVENTORY CONTEINER SHOP UI")]    
    [SerializeField] protected Transform _inventoryShopConteiner;
    [SerializeField] protected GameObject _prefabInventoryShopItemUI;

    [Header ("SHOP CONTEINER UI")]    
    [SerializeField] protected Transform _SkinShopConteiner;
    [SerializeField] protected List<GameObject> _prefabSkinShopItemUI;

    public static Store Instance;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
        SetSkins();  // Inicializa las skins disponibles en la tienda
    }

    #region Player Detection

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ListItemsOnInventoryShop();
            _gameManager.ToggleShopPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameManager.ToggleShopPanel();
        }
    }

    #endregion

    #endregion

    #region Inventory Methods

    //Agrega Items a la tienda
    public void Add(Item_ScriptableObject item)
    {
        _items_SO.Add(item);
    }

    //Remueve Items a la tienda
    public void Remove(Item_ScriptableObject item)
    {
        _items_SO.Remove(item);
    }

    #endregion

    #region ShopItemStore Methods
    public void ListItemsOnInventoryShop()
    {
        // Destruye los items previos en la UI de la tienda
        foreach (Transform child in _inventoryShopConteiner)
        {
            Destroy(child.gameObject);
        }

        // Instancia y configura cada ítem en la tienda
        foreach (Item_ScriptableObject item in _items_SO)
        {
            GameObject go = Instantiate(_prefabInventoryShopItemUI, _inventoryShopConteiner);
            ShopItemStore shopItemUI = go.GetComponent<ShopItemStore>();

            if (shopItemUI != null)
            {
                shopItemUI.Setup(item);
            }
            else
            {
                Debug.LogWarning("El prefab no tiene asignado el componente ShopItemStore.");
            }
        }
    }

    public void SetSkins()
    {
        foreach (GameObject skin in _prefabSkinShopItemUI)
        {
            ShopSkinStore shopSkinUI = skin.GetComponent<ShopSkinStore>();

            if(shopSkinUI != null)
            {
                shopSkinUI.Setup(shopSkinUI.GetSkisSO);
            }
            else
            {
                Debug.LogWarning("El GameObject no tiene el componente ShopSkinStore asignado.");
            }
        }
    }

    //Vende items, actualiza monedas y lo elimina del inventoario
    public void SellItem(Item_ScriptableObject item)
    {
        Inventory.Instance.Remove(item);
        _items_SO.Remove(item);
        _currencyManager.AddCoins(item.itemPrice);
        Debug.Log(item.itemPrice);
        ListItemsOnInventoryShop();
    }

    //Metodo de compra 
    public void BuyItem(Skin_ScriptableObject skin, ShopSkinStore shopSkinUI)
    {
        if (_currencyManager.CanAfford(skin.skinPrice))
        {
            _currencyManager.SpendCoins(skin.skinPrice);

            shopSkinUI.GetPriceButton.gameObject.SetActive(false);
            shopSkinUI.GetEquipButton.gameObject.SetActive(true);
            Debug.Log("Skin comprada: " + skin.skinName);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar: " + skin.skinName);
        }
    }

    #endregion

}
