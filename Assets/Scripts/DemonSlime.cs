using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSlime : MonoBehaviour  // Clase que acompa�a al enemigo
{
    public int runEnemy;
    public Transform transformPlayer;
    [Range(1, 5)] public int vida;

    private Rigidbody2D rb2D;
    private Transform transformEnemy;
    private SpriteRenderer sprtEnemy;
    private Animator animEnemy;
    private int factorX;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        transformEnemy = GetComponent<Transform>();
        animEnemy = GetComponent<Animator>();
        sprtEnemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMov();
        
    }

        private void OnCollisionEnter2D(Collision2D collision) // Cambia a OnTriggerEnter2D si usas triggers
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


    private void checkMov() {
        if (transformPlayer.position.x - transformEnemy.position.x <= 0)
        {
            factorX = -1;
            sprtEnemy.flipX = false;
        }
        else
        {
            factorX = 1;
            sprtEnemy.flipX = true;
        }

        if (CheckArea.checkFollow)
        {
            rb2D.velocity = new Vector2(factorX * runEnemy, rb2D.velocity.y);
            animEnemy.SetBool("isRunning", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animEnemy.SetBool("isRunning", false);
        }
    }
public void DestroyEnemy()
{
    Destroy(gameObject); // Destruye el objeto enemigo y lo llamamos como evento al final de la animacion de morir.
}
private void Bloquear(){
            rb2D.velocity = Vector2.zero; // Detener el movimiento
            rb2D.isKinematic = true; // Evitar que la gravedad afecte al jugador
            GetComponent<Collider2D>().enabled = false; // Desactivar el collider
            this.enabled = false;  // Opcional: desactivar el script de movimiento si lo tienes
    }


}
