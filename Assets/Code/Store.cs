using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Store : MonoBehaviour
{
    [SerializeField] protected List<Item_ScriptableObject> _items_SO = new List<Item_ScriptableObject>();
    public static Store Instance;
    [SerializeField] protected Transform _inventoryShopConteiner;
    [SerializeField] protected GameObject _prefabInventoryShopItemUI;
    [SerializeField] protected CurrencyManager _currencyManager;

    [SerializeField] protected Transform _SkinShopConteiner;
    [SerializeField] protected List<GameObject> _prefabSkinShopItemUI;

    private void Awake()
    {
        Instance = this;
        SetSkins();
    }
    public void Add(Item_ScriptableObject item)
    {
        _items_SO.Add(item);
    }

    public void Remove(Item_ScriptableObject item)
    {
        _items_SO.Remove(item);
    }
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
                Debug.LogWarning("El prefab no tiene asignado el componente ShopItemUI.");
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

    public void SellItem(Item_ScriptableObject item)
    {
        Inventory.Instance.Remove(item);
        _items_SO.Remove(item);
        _currencyManager.AddCoins(item.itemPrice);
        Debug.Log(item.itemPrice);
        ListItemsOnInventoryShop();
    }

    public void BuyItem(Skin_ScriptableObject skin, ShopSkinStore shopSkinStore)
    {
        if (_currencyManager.CanAfford(skin.skinPrice))
        {
            _currencyManager.SpendCoins(skin.skinPrice);

            shopSkinStore.GetEquipButton.gameObject.SetActive(true);
            Debug.Log("Skin comprada: " + skin.skinName);
            shopSkinStore.GetPriceButton.gameObject.SetActive(false);

            // Podrías, por ejemplo, desactivar el botón:
            // shopSkinUI.DisableBuyButton(); // método que podrías implementar en ShopSkinStore
        }
        else
        {
            Debug.Log("No tienes suficientes monedas para comprar: " + skin.skinName);
        }
    }
}
