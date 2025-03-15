using UnityEngine.UI;
using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] protected List<Item_ScriptableObject> _items_SO = new List<Item_ScriptableObject>();

    [SerializeField] protected Transform _itemContent;
    [SerializeField] protected GameObject _prefabInventoryItemUI;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item_ScriptableObject item)
    {
        _items_SO.Add(item);
    }

    public void Remove(Item_ScriptableObject item)
    {
        _items_SO.Remove(item);
    }

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

    public List<Item_ScriptableObject>  GetLengthInventory
    {
        get { return _items_SO; }
    }
}
