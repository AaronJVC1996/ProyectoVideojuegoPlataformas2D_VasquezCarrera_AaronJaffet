using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public GameObject ballPrefab;

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprtrRnd;
    private Transform trfm;
    private float lastShoot;
    private float waitShootTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprtrRnd = GetComponent<SpriteRenderer>();
        trfm = GetComponent<Transform>();
        waitShootTime = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMovement();
        checkJump();
       // Shoot();
       // Debug.Log(ChechGround.isGrounded);
    }

   // private void Shoot() 
    //{
      //  if (Input.GetKey(KeyCode.E) && (Time.time > lastShoot + waitShootTime))
        //{
         //   object newBall = Instantiate(ballPrefab, trfm.position, Quaternion.identity);
           // lastShoot = Time.time;
       // }
   // }

    private void checkJump() 
    {
        if (Input.GetKey(KeyCode.Space))   // && CheckGround.isGrounded
        {
           rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
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

}