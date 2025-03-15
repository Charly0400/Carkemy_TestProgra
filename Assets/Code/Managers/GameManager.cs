using System.Collections;
using UnityEngine;
// Gestiona la lógica general del juego, incluyendo la UI y efectos de fade.

public class GameManager : MonoBehaviour
{
    #region Variables

    [Header("INVENTORY PANEL")]
    [SerializeField] protected GameObject _inventoryPanel;
    [SerializeField] protected bool isInventoryPanel;

    [Header("SHOP PANEL")]
    [SerializeField] protected GameObject _shopPanel;
    [SerializeField] protected bool isShopActive;

    [Header("FADE IN & FADE OUT REFERENCES")]
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeDuration = 1f;

    #endregion

    #region Public Methods

    // Muestra u oculta el panel de inventario si la tienda no está activa.
    public void ToggleInventoryPanel()
    {
        if (!isShopActive)
        {
            isInventoryPanel = !isInventoryPanel;
            _inventoryPanel.SetActive(isInventoryPanel);
        }
    }

    // Muestra u oculta el panel de la tienda.
    public void ToggleShopPanel()
    {
        isShopActive = !isShopActive;
        _shopPanel.SetActive(isShopActive);
    }

    #endregion

    #region Fade In & Out

    // Efecto de fade out para transiciones.
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

    // Efecto de fade in para transiciones.
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

    #endregion
}
