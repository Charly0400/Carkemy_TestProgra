using UnityEngine;

public class Skin_Manager : MonoBehaviour
{
    public static Skin_Manager Instance;
    public Animator characterAnimator; 
    public Skin_ScriptableObject currentSkin;

    private void Awake()
    {
        Instance = this;
    }

    public void EquipSkin(Skin_ScriptableObject skin)
    {
        currentSkin = skin;
        if (skin.animatorOverrideController != null)
        {
            characterAnimator.runtimeAnimatorController = skin.animatorOverrideController;
            Debug.Log("Skin equipada: " + skin.skinName);
            Debug.Log("Skin equipada: " + skin.animatorOverrideController);
        }
        else
        {
            Debug.LogWarning("La skin " + skin.skinName + " no tiene asignado un AnimatorOverrideController.");
        }
    }


}
