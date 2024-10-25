using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour // Clase que acompa�a al jugador
{
    public float runSpeed;
    public float jumpSpeed;
   // public GameObject ballPrefab;

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprtrRnd;
    private Transform trfm;
   private bool isAttacking = false;
    private int attackIndex = 0;
    private float timeBetweenAttacks = 0.3f;
    private float lastAttackTime = 0f;
    // Start is called before the first frame update
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
        Debug.Log(CheckGround.isGrounded);
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

   private void checkAttack(){
        if (Input.GetKeyDown(KeyCode.V)) // Cambia "J" al botón que prefieras para atacar
        {
            if (Time.time - lastAttackTime > timeBetweenAttacks)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private void Attack(){
    if (!isAttacking)
    {
        anim.SetTrigger("Attack1");
        isAttacking = true; 
        attackIndex = 1;
    }
    else if (attackIndex == 1)
    {
        anim.SetTrigger("Attack2");
        attackIndex = 0;
    }
}

 public void ResetAttack(){
        isAttacking = false;
        attackIndex = 0;
    }
}