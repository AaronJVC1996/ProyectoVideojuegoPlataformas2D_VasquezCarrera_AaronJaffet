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
    public float attackRange;
    public GameObject enemySwordPrefab; // Prefab de la espada del enemigo
    public Transform attackPoint;
    private bool isAttacking = false;
     private CheckArea checkArea;
     public GameObject portalObject;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        transformEnemy = GetComponent<Transform>();
        animEnemy = GetComponent<Animator>();
        sprtEnemy = GetComponent<SpriteRenderer>();
        checkArea = GetComponentInChildren<CheckArea>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMov();
        
    }

        private void OnCollisionStay2D(Collision2D collision) // OnCollisionStay2D para que no tengas que separarte y volver a pegarte al collider para recibir daño
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


 private void checkMov() 
   {
         if (transformPlayer == null)
    {
        // Si el jugador no existe, detener el movimiento del enemigo y salir de la función
        rb2D.velocity = Vector2.zero;
        animEnemy.SetBool("isRunning", false);
        return;
    }

    float distanceToPlayer = Mathf.Abs(transformPlayer.position.x - transformEnemy.position.x);

    // Determina la dirección en función de la posición del jugador
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

    Vector3 attackPointPosition = attackPoint.localPosition;
    if (sprtEnemy.flipX)
    {
        attackPointPosition.x = 4f;
    }
    else
    {
        attackPointPosition.x = -4f;
    }
    attackPoint.localPosition = attackPointPosition;

    if (checkArea != null && checkArea.checkFollow)
    {
        if (!isAttacking)
        {
            if (distanceToPlayer <= attackRange)
            {
                isAttacking = true;
                animEnemy.SetTrigger("isAttack");
                rb2D.velocity = Vector2.zero;
            }
            else
            {
                rb2D.velocity = new Vector2(factorX * runEnemy, rb2D.velocity.y);
                animEnemy.SetBool("isRunning", true);
            }
        }
    }
    else
    {
        rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        animEnemy.SetBool("isRunning", false);
    }
}
     public void EndAttack()
    {
        isAttacking = false; // Permite que el enemigo vuelva a moverse después de atacar
    }

public void DestroyEnemy()
{
    portalObject.SetActive(true);
    Destroy(gameObject); // Destruye el objeto enemigo y lo llamamos como evento al final de la animacion de morir.
}
private void Bloquear(){
            rb2D.velocity = Vector2.zero; // Detener el movimiento
            rb2D.isKinematic = true; // Evitar que la gravedad afecte al jugador
            GetComponent<Collider2D>().enabled = false; // Desactivar el collider
            this.enabled = false;  // Opcional: desactivar el script de movimiento si lo tienes
    }
    
    private void Attack()
    {
        // Instancia la espada del enemigo en el punto de ataque
        GameObject swordInstance = Instantiate(enemySwordPrefab, attackPoint.position, Quaternion.identity);
           

        Destroy(swordInstance, 0.1f); // Destruye la espada después de un tiempo
    }
}
