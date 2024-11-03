using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour // Clase que acompa�a al jugador
{
    public float runSpeed;
    public float jumpSpeed;
  public int vida;

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprtrRnd;
    private Transform trfm;

 private float timeBetweenAttacks = 0.25f; // Tiempo entre ataques
    private float lastAttackTime = 0f;
    private bool canAttack2 = false; // Indica si se puede activar Attack2
    public GameObject PlayerAttackPrefab; // Asigna tu prefab aquí en el Inspector
public Transform AttackPoint; // El objeto de referencia donde se iniciara el ataque

 private float timeBetweenDamage = 0.5f; // Tiempo entre daños
    private float lastDamageTime = 0f; // Último tiempo de daño
    private Color originalColor;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprtrRnd = GetComponent<SpriteRenderer>();
        trfm = GetComponent<Transform>();
        originalColor = sprtrRnd.color;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMovement();
        checkJump();
         checkFalling();
        checkAttack();
        
    }

      private void checkJump()
    {
        if (Input.GetKey(KeyCode.Space) && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            anim.SetBool("isJumping", true); // Activamos la animación de Jumpstart
        }
    }

    private void checkFalling()
    {
        // Si la velocidad en el eje Y es negativa (el personaje está cayendo)
        if (rb2D.velocity.y < 0)
        {
            anim.SetBool("isJumping", false); // Desactivamos la animación de subida
            anim.SetBool("isFalling", true);  // Activamos la animación de Jumpend
        }

        // Si el personaje ha llegado al suelo
        if (CheckGround.isGrounded)
        {
            anim.SetBool("isFalling", false); // Desactivamos la animación de caída
        }
    }

private void checkMovement() 
{
    float horizontalInput = 0f; // Variable para almacenar la entrada horizontal

    if (Input.GetKey(KeyCode.A))
    {
        horizontalInput = -runSpeed; // Movimiento a la izquierda
        anim.SetBool("isRunning", true);
        sprtrRnd.flipX = true; // Voltea el sprite
    }
    else if (Input.GetKey(KeyCode.D))
    {
        horizontalInput = runSpeed; // Movimiento a la derecha
        anim.SetBool("isRunning", true);
        sprtrRnd.flipX = false; // No voltea el sprite
    }
    else
    {
        anim.SetBool("isRunning", false);
    }

    // Ajusta la velocidad del Rigidbody y la posición del AttackPoint
    rb2D.velocity = new Vector2(horizontalInput, rb2D.velocity.y);

    // Ajusta la posición del AttackPoint
    Vector3 attackPointPosition = AttackPoint.localPosition;
    if (sprtrRnd.flipX) // Si el sprite está mirando a la izquierda
    {
        attackPointPosition.x = -1f; // Ajusta la posición a la izquierda
    }
    else // Si el sprite está mirando a la derecha
    {
        attackPointPosition.x = 1f; // Ajusta la posición a la derecha
    }
    AttackPoint.localPosition = attackPointPosition; // Aplica la nueva posición
}

  private void checkAttack()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Time.time - lastAttackTime >= timeBetweenAttacks)
            {
                
            GameObject attackInstance = Instantiate(PlayerAttackPrefab, AttackPoint.position, Quaternion.identity);
            
            Destroy(attackInstance, 0.25f); // Elimina el ataque tras un corto tiempo
                if (!canAttack2) // Si no se puede atacar 2, realiza Attack1
                {
                    anim.SetTrigger("Attack1");
                    lastAttackTime = Time.time; // Reinicia el tiempo de ataque
                    canAttack2 = true; // Ahora se puede activar Attack2
                }
                else // Si ya se realizó Attack1, activa Attack2
                {
                    anim.SetTrigger("Attack2");
                    lastAttackTime = Time.time; // Reinicia el tiempo de ataque
                    canAttack2 = false; // Resetea para volver a Attack1 en el siguiente ataque
                }
            }
        }
    }
     public void TakeDamage(int damage)
    {
        // Solo permite recibir daño si ha pasado el tiempo entre daños
        if (Time.time - lastDamageTime >= timeBetweenDamage)
        {
            vida -= damage; // Reduce la vida del jugador
            lastDamageTime = Time.time; // Actualiza el tiempo del último daño
            FlashWhite(); // Mi animacion de recibir daño inventada ya que no venia en mis assets
            

            // anim.SetTrigger("TakeHit"); // No tenemos animacion de recibir daño pero invete un cambio de color a rojo con un intervalo de tiempo para hacer de animacion de recivir daño

            if (vida <= 0)
            {
                anim.SetTrigger("Death");
            }
        }
    }
    

    private void Die() // puesto al final de la animacion de muerte.
    {
         // Activa la animación de muerte
        // Aquí puedes añadir lógica adicional como desactivar el jugador o reiniciar el nivel
        Destroy(gameObject); // Destruye el objeto después de un segundo (ajusta según sea necesario)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Bloquear(){ //puesto al inicio de la animacion de muerte como evento
            rb2D.velocity = Vector2.zero; // Detener el movimiento
            rb2D.isKinematic = true; // Evitar que la gravedad afecte al jugador
            GetComponent<Collider2D>().enabled = false; // Desactivar el collider
            this.enabled = false;  // Opcional: desactivar el script de movimiento si lo tienes
    }


    public void FlashWhite()
{
    StartCoroutine(FlashCoroutine());
}


private IEnumerator FlashCoroutine()
{
    // Cambia el color a blanco
    sprtrRnd.color = Color.red;
    // Espera 0.5 segundos
    yield return new WaitForSeconds(0.05f);
    // Restaura el color original
    sprtrRnd.color = originalColor; // Aquí puedes establecer el color original si es diferente
    yield return new WaitForSeconds(0.05f);
    sprtrRnd.color = Color.red;
    yield return new WaitForSeconds(0.05f);
    sprtrRnd.color = originalColor;
}
}
