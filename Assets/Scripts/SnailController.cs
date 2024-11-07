using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
   [Range(1, 5)] public int vida;
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float movementRange = 3f; // Rango de movimiento
    private float startX; // Posición inicial en X
    private bool movingRight = true; // Dirección de movimiento
    private Animator animator; // Referencia al Animator
    private SpriteRenderer sprtrRnd; // Referencia al SpriteRenderer
    private Rigidbody2D rb2D;
    private float moveTime = 6f; // Tiempo total de movimiento antes de hacer idle
    private float idleTime = 3f; // Tiempo que permanece en idle
    private float timeMoving = 0f; // Contador para el tiempo de movimiento
    private bool isIdle = false; // Control para saber si ya está en idle
    public int damage = 1;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        startX = transform.position.x; // Guarda la posición inicial
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
        sprtrRnd = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer
    }

    private void Update()
    {
        if (!isIdle)
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
    }

    private void Move()
    {
        // Mueve al caracol de izquierda a derecha
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true); // Activa la animación de movimiento
            sprtrRnd.flipX = true; // Asegúrate de que el sprite esté mirando a la derecha
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            animator.SetBool("isWalking", true); // Activa la animación de movimiento
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
        isIdle = true; // Marca que el caracol está en idle
        animator.SetBool("isWalking", false);
        animator.SetTrigger("SnailStop"); // Activa la animación de Idle una vez
        yield return new WaitForSeconds(idleTime); // Espera el tiempo de idle
        timeMoving = 0f; // Reinicia el tiempo de movimiento
        isIdle = false; // Permite que vuelva a moverse
    }
        public void Die()
    {
        Destroy(gameObject); // Destruye el objeto después de 1 segundo (puedes ajustar el tiempo)
    }
    private void Bloquear(){
            rb2D.velocity = Vector2.zero; // Detener el movimiento
            rb2D.isKinematic = true;
            GetComponent<Collider2D>().enabled = false; // Desactivar el collider
            this.enabled = false;  // Opcional: desactivar el script de movimiento si lo tienes
    }
}