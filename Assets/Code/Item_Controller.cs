using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    [SerializeField] protected Item_ScriptableObject _items_SO;

    protected void PickUp()
    {
        Inventory.Instance.Add(_items_SO);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_items_SO != null && collision.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
}
