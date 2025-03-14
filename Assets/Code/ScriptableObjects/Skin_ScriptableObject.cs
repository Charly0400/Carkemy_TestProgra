using UnityEngine;

[CreateAssetMenu(menuName = "Skins / New Skin")]
public class Skin_ScriptableObject : ScriptableObject
{
    [Header("NAME")]
    public string skinName;

    [Header("SPRITE")]
    public Sprite skinSprite;

    [Header("PRICE")]
    public int skinPrice;

    [Header("Animator")]
    public AnimatorOverrideController animatorOverrideController;
}
