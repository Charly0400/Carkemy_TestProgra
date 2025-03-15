using UnityEngine;
using System.Collections.Generic;

public class GenericEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float followSpeed = 3f;
    public Transform player; // Asigna el transform del jugador o se buscará por tag "Player"

    [Header("Health")]
    public int health = 3;

    [Header("Drops")]
    [Tooltip("Lista de ScriptableObjects de ítems que se pueden soltar al morir.")]
    public List<Item_ScriptableObject> dropItems;

    [Header("Animations")]
    public Animator animator; // Asigna el Animator del enemigo

    private Rigidbody2D rb;

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

            // Reproduce la animación de caminar
            if (animator != null)
            {
                animator.Play("Walk");
            }
        }
    }

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
        // Permite que se reproduzca la animación de muerte (por ejemplo, 1 segundo)
        Destroy(gameObject, 1f);
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
                // Instancia el prefab del objeto drop.
                // Opcional: si tienes una zona de recogida, puedes asignar su transform como padre.
                Instantiate(itemToDrop.dropPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No se encontró un prefab de drop en el ScriptableObject: " + itemToDrop.itemName);
            }
        }
    }
}