using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject _inventoryPanel;
    [SerializeField] protected bool isInventoryPanel;

    [SerializeField] protected GameObject _shopPanel;
    [SerializeField] protected bool isShopActive;

    public void ToggleInventoryPanel()
    {
        if (!isShopActive)
        {
            isInventoryPanel = !isInventoryPanel;
            _inventoryPanel.SetActive(isInventoryPanel);
        }
    }

    public void ToggleShopPanel()
    {
        isShopActive = !isShopActive;
        _shopPanel.SetActive(isShopActive);
    }
}
