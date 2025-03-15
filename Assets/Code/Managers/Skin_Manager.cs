using UnityEngine;

//Gestiona la selección y aplicación de skins para el personaje.
public class Skin_Manager : MonoBehaviour
{

    #region Variables

    public static Skin_Manager Instance;

    [Header("REFERENCES")]
    [SerializeField] protected Animator characterAnimator; 
    [SerializeField] protected Skin_ScriptableObject currentSkin;

    #endregion

    #region Unity Metods

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region Public Methods
    //Actualiza una nueva skin y el animator
    public void EquipSkin(Skin_ScriptableObject skin)
    {
        currentSkin = skin;
    
        if (skin.animatorOverrideController != null)
        {
            characterAnimator.runtimeAnimatorController = skin.animatorOverrideController;
        }
        else
        {
            Debug.LogWarning("The Skin " + skin.skinName + " doesn't have an AnimatorOverrideController.");
        }
    }

    #endregion
}
