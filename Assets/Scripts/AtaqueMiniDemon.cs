using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMiniDemon : MonoBehaviour
{

    public int damage = 1; // Daño del ataque

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica si el collider es el jugador
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage); // Llama al método para restar vida al jugador
                // Aquí puedes agregar una animación si lo deseas
            }
        }
    }
}