using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : MonoBehaviour
{
    [Range(1, 5)] public int vida;
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float movementRange = 3f; // Rango de movimiento
    private float startX; // Posición inicial en X
    private bool movingRight = true; // Dirección de movimiento
    private Animator animator; // Referencia al Animator
    private SpriteRenderer sprtrRnd; // Referencia al SpriteRenderer
    private float moveTime = 6f; // Tiempo total de movimiento antes de hacer idle
    private float idleTime = 2f; // Tiempo que permanece en idle
    private float timeMoving = 0f; // Contador para el tiempo de movimiento
    public int damage = 1;
    

    private void Start()
    {
        startX = transform.position.x; // Guarda la posición inicial
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
        sprtrRnd = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer
        
    }

    private void Update()
    {
        if (timeMoving < moveTime)
        {
            Move(); // Llama a la función de movimiento
            timeMoving += Time.deltaTime; // Incrementa el tiempo de movimiento
        }
        else
        {
            StartCoroutine(Idle()); // Cambia a idle
        }
    }

    private void Move()
    {
        // Mueve al MiniDemon de izquierda a derecha
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true); // Activa la animación de vuelo
            sprtrRnd.flipX = true; // Asegúrate de que el sprite esté mirando a la derecha
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            animator.SetBool("isRunning", true); // Activa la animación de vuelo
            sprtrRnd.flipX = false; // Voltea el sprite cuando se mueve a la izquierda
        }

        // Cambia de dirección si alcanza el rango
        if (transform.position.x >= startX + movementRange)
        {
            movingRight = false; // Cambia la dirección
        }
        else if (transform.position.x <= startX - movementRange)
        {
            movingRight = true; // Cambia la dirección
        }
    }

    private void OnCollisionStay2D(Collision2D collision) // Cambia a OnTriggerEnter2D si usas triggers
    {
        if (collision.gameObject.CompareTag("Player")) // Asegúrate de que tu jugador tenga el tag "Player"
        {
            PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Llama al método TakeDamage del jugador
            }
        }
    }

    private IEnumerator Idle()
    {
        animator.SetBool("isRunning", false); // Para la animación de vuelo
        yield return new WaitForSeconds(idleTime); // Espera el tiempo de idle
        timeMoving = 0f; // Reinicia el tiempo de movimiento
    }
}