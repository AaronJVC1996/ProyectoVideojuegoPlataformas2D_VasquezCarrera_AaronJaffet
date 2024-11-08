using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBeeController : MonoBehaviour
{
    public Transform transformPlayer;    // Referencia al jugador
    public float followSpeed = 5f;       // Velocidad de seguimiento
    public float followDistance = 1f;    // Distancia mínima alrededor del jugador
    [Range(1, 5)] public int vida;              // Vida del enemigo
    public int damage = 1;               // Daño que el enemigo aplica al jugador
    private Rigidbody2D rb2D;            // Rigidbody del enemigo
    private Animator animEnemy;          // Animator del enemigo
    private SpriteRenderer sprtEnemy;    // Sprite renderer del enemigo

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animEnemy = GetComponent<Animator>();
        sprtEnemy = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (transformPlayer == null) return;

        // Calcula la dirección y la distancia entre el enemigo y el jugador
        Vector2 direction = (transformPlayer.position - transform.position).normalized;
        rb2D.velocity = direction * followSpeed;
        sprtEnemy.flipX = direction.x > 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Aplica daño solo si está colisionando con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerHealth = collision.gameObject.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}