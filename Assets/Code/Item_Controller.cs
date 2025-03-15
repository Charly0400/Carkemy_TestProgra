using UnityEngine;

//Controla el comportamiento de los onjetos recogidos por el jugador
public class Item_Controller : MonoBehaviour
{
    #region Varibale

    [Header("REFERENCES")]
    [SerializeField] protected Item_ScriptableObject _items_SO;

    #endregion

    #region Item Controller
    //Añade el ítem al inventario y a la tienda, y luego destruye el objeto en la escena.
    protected void PickUp()
    {
        Inventory.Instance.Add(_items_SO);
        Store.Instance.Add(_items_SO);
        Destroy(gameObject);
    }

    //verifica que la colisión con el jugador al igual que verifica so el inventario tiene espacio
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_items_SO != null && collision.gameObject.CompareTag("Player"))
        {
            if(Inventory.Instance.GetLengthInventory.Count <= 14)
                PickUp();
        }
    }

    #endregion
}
