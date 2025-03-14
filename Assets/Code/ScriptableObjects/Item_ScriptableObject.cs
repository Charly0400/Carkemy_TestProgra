using UnityEngine;

[CreateAssetMenu(menuName = "Items / New Item")]
public class Item_ScriptableObject : ScriptableObject
{
    [Header("NAME")]
    public string itemName;
        
    [Header("SPRITE")]
    public Sprite itmeSprite;

    [Header("PRICE")]
    public int itemPrice;

}
