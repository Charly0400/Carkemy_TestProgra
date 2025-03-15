using System.Collections.Generic;
using UnityEngine;

//Moviemnto del enemy
public class GenericEnemy : MonoBehaviour
{

    #region Variables

    [Header("MOVEMENT")]
    [SerializeField] protected float followSpeed = 3f;
    [SerializeField] protected Transform player; 

    [Header("HEALTH")]
    [SerializeField] protected int health = 3;

    [Header("DROPS")]
    [SerializeField]  protected List<Item_ScriptableObject> dropItems;

    [Header("ANIMATION")]
    [SerializeField] protected Animator animator;

    [Header("REFERENCES")]
    [SerializeField] protected Rigidbody2D rb;

    #endregion

    #region Unity Methods

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    private void Update()
    { 
        if (player != null)
        {
            // Calcula la dirección hacia el jugador y mueve al enemigo
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * followSpeed;

            // Ajusta la escala para que el enemigo mire hacia el jugador
            if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
    }

    #endregion

    #region Private Methods
    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Lógica de muerte: reproducir animación, soltar un objeto y destruirse
    public void Die()
    {
        if (animator != null)
        {
            animator.Play("Death");
        }
        rb.linearVelocity = Vector2.zero;
        DropRandomItem();
        Destroy(gameObject, 1);
    }

    // Selecciona un ítem aleatorio de la lista de drops y lo instancia en la posición del enemigo
    private void DropRandomItem()
    {
        if (dropItems != null && dropItems.Count > 0)
        {
            int randomIndex = Random.Range(0, dropItems.Count);
            Item_ScriptableObject itemToDrop = dropItems[randomIndex];

            if (itemToDrop != null && itemToDrop.dropPrefab != null)
            {
                Instantiate(itemToDrop.dropPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No se encontró un prefab de drop en el ScriptableObject: " + itemToDrop.itemName);
            }
        }
    }

    #endregion

    #region Colision
    //Gestiona el daño provocado por el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            TakeDamage(3);
        }
    }

    #endregion
}