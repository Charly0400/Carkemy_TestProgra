using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject _inventoryPanel;
    [SerializeField] protected bool isInventoryPanel;

    public void ToggleInventoryPanel()
    {
        isInventoryPanel = !isInventoryPanel;
        _inventoryPanel.SetActive(isInventoryPanel);
    }

}
