using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculosDamage : MonoBehaviour
{
    public int damage = 1; // Daño del ataque
private void OnTriggerStay2D(Collider2D collision) // OnCollisionStay2D para que no tengas que separarte y volver a pegarte al collider para recibir daño
    {
        if (collision.CompareTag("Player")) // Asegúrate de que tu jugador tenga el tag "Player"
        {
            PlayerController playerHealth = collision.GetComponent<PlayerController>();
            playerHealth.TakeDamage(damage); // Llama al método TakeDamage del jugador
        }
    }
}
