using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour{

private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        
        if (player != null)
        {
                collision.GetComponent<Animator>().SetTrigger("Death");
        }
    }
}
}
    

