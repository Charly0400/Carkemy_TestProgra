using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject _inventoryPanel;
    [SerializeField] protected bool isInventoryPanel;

    [SerializeField] protected GameObject _shopPanel;
    [SerializeField] protected bool isShopActive;

    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeDuration = 1f;

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

    public IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
