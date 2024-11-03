using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDemon : MonoBehaviour
{
    [Range(1, 5)] public int vida;
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float movementRange = 3f; // Rango de movimiento
    private float startX; // Posición inicial en X
    private bool movingRight = true; // Dirección de movimiento
    private Animator animator; // Referencia al Animator
    public GameObject fireballPrefab; // Prefab de la bola de fuego
    public Transform fireballSpawnPoint; // Punto donde se lanzará la bola de fuego
    private SpriteRenderer sprtrRnd; // Referencia al SpriteRenderer
    private Rigidbody2D rb2D;

    private float moveTime = 6f; // Tiempo total de movimiento antes de hacer idle
    private float idleTime = 2f; // Tiempo que permanece en idle
    private float timeMoving = 0f; // Contador para el tiempo de movimiento
    public int damage = 1;
    private CheckArea checkArea;

    private void Start()
    {
        checkArea = GetComponentInChildren<CheckArea>();
        rb2D = GetComponent<Rigidbody2D>();
        startX = transform.position.x; // Guarda la posición inicial
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
        sprtrRnd = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer
        StartCoroutine(FireballAttack()); // Comienza a lanzar bolas de fuego
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

    private IEnumerator FireballAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f); // Espera 4 segundos
             if (checkArea.checkFollow) // Verifica si el jugador está en rango
        {
            Fireball(); // Lanza la bola de fuego
        }
        }
    }


private void Fireball()
{
    GameObject fireballInstance = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
    animator.SetTrigger("isAttack");
    
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        Vector2 direction = (player.transform.position - fireballInstance.transform.position).normalized; 
        Rigidbody2D rb = fireballInstance.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.velocity = direction * 10f; // Ajusta la velocidad aquí
        }

        // Flip the fireball based on the direction
        if (direction.x < 0)
        {
            // Si el jugador está a la izquierda, voltea la bola de fuego
            fireballInstance.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            // Si el jugador está a la derecha, usa la escala original
            fireballInstance.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    
    Destroy(fireballInstance, 4f); // Destruir la bola de fuego después de 5 segundos
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