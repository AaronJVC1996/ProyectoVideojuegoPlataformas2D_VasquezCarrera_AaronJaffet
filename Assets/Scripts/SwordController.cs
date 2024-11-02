using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
     private void OnCollisionEnter2D(Collision2D collision) // OnCollisionEnter2D (Collision2D) si el Collider no esta en trigger (asi empuja a los enemigos) y OnTriggerEnter2D (Collider2D) si este esta en trigger y no empujara a los enemigos
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DemonSlime enemy = collision.gameObject.GetComponent<DemonSlime>(); // Si el collider es un trigger deberemos quitar la parte de gameObject
            if (enemy != null)
            {
                enemy.vida--; // Reduce la vida del enemigo
                if (enemy.vida <= 0)
                {
                    
                    collision.gameObject.GetComponent<Animator>().SetTrigger("Death");
                }
                else
                {
                     collision.gameObject.GetComponent<Animator>().SetTrigger("TakeHit"); // Animación de hit si aún tiene vida, Si el collider es un trigger deberemos quitar la parte de gameObject
                }
            }
        }
    }
}