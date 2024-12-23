using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
                if (collision.CompareTag("Ground")) // Verifica si el objeto tiene el tag "Ground"
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
               if (collision.CompareTag("Ground")) // Verifica si el objeto tiene el tag "Ground"
        {
            isGrounded = false;
        }
    }
}