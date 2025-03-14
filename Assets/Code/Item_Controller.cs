using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    [SerializeField] protected Item_ScriptableObject _items_SO;
    [SerializeField] protected Inventory _inventory;

    protected void PickUp()
    {
        Inventory.Instance.Add(_items_SO);
        Store.Instance.Add(_items_SO);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_items_SO != null && collision.gameObject.CompareTag("Player"))
        {
            if(_inventory.GetLengthInventory.Count <= 14)
                PickUp();
        }
    }
}
