using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public Transform AttackPoint;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprtrRnd = GetComponent<SpriteRenderer>();
        trfm = GetComponent<Transform>();
       
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
        if (Input.GetKey(KeyCode.A))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            anim.SetBool("isRunning", true);
            sprtrRnd.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            anim.SetBool("isRunning", true);
            sprtrRnd.flipX = false;
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            anim.SetBool("isRunning", false);
        }
    }

  private void checkAttack()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Time.time - lastAttackTime >= timeBetweenAttacks)
            {
            GameObject attackInstance = Instantiate(PlayerAttackPrefab, AttackPoint.position, Quaternion.identity);
            Destroy(attackInstance, 0.5f); // Elimina el ataque tras un corto tiempo
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
        vida -= damage; // Reduce la vida del jugador
        //anim.SetTrigger("TakeHit"); // No tenemos animacion de recivir daño

        if (vida <= 0)
        {
            anim.SetTrigger("Death");
        }
    }
    

    private void Die()
    {
         // Activa la animación de muerte
        // Aquí puedes añadir lógica adicional como desactivar el jugador o reiniciar el nivel
        Destroy(gameObject); // Destruye el objeto después de un segundo (ajusta según sea necesario)
    }
    private void Bloquear(){
            rb2D.velocity = Vector2.zero; // Detener el movimiento
            rb2D.isKinematic = true; // Evitar que la gravedad afecte al jugador
            GetComponent<Collider2D>().enabled = false; // Desactivar el collider
            this.enabled = false;  // Opcional: desactivar el script de movimiento si lo tienes
    }
}
