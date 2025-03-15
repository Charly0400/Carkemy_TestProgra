using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Gestiona el inventario del jugador

public class Inventory : MonoBehaviour
{
    #region Variables

    [Header("LIST OF ITEMS")]
    [SerializeField] protected List<Item_ScriptableObject> _items_SO = new List<Item_ScriptableObject>();

    [Header("INVENTORY UI")]
    [SerializeField] protected Transform _itemContent;
    [SerializeField] protected GameObject _prefabInventoryItemUI;

    public static Inventory Instance;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Inventory Methods
    //Agrega Items al inventario
    public void Add(Item_ScriptableObject item)
    {
        _items_SO.Add(item);
    }
    //Remueve Items del inventario
    public void Remove(Item_ScriptableObject item)
    {
        _items_SO.Remove(item);
    }

    //Lista los intems en la UI del inventario
    public void ListItems()
    {
        foreach (Transform item in _itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item_ScriptableObject item in _items_SO)
        {
            GameObject gO = Instantiate(_prefabInventoryItemUI, _itemContent);
            var itemSprite = gO.transform.Find("SpriteItem").GetComponent<Image>();
            var itemPrice = gO.transform.Find("PriceItem").GetComponent<TextMeshProUGUI>();
            var itemName = gO.transform.Find("NameItem").GetComponent<TextMeshProUGUI>();

            itemName.text = item.itemName;
            itemSprite.sprite = item.itmeSprite;
            itemPrice.text = item.itemPrice.ToString();

        }
    }

    #endregion

    #region Getter

    public List<Item_ScriptableObject>  GetLengthInventory
    {
        get { return _items_SO; }
    }

    #endregion
}
