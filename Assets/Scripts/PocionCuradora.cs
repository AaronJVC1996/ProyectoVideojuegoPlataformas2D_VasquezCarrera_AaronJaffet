using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocionCuradora : MonoBehaviour
{
    public int cantidadCura = 5; // Cantidad de vida que la poción restaurará 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprueba si el objeto que entra en el trigger tiene el tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Accede al componente PlayerController del jugador
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeHeal(cantidadCura); // Cura al jugador
                Destroy(gameObject); // Destruye la poción después de usarla
            }
        }
    }

}

