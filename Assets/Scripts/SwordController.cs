using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Se que podria haber usado herencia para esto y me evitaba tanto codigo aqui, pero aun no sabia hacerlo y ya estaba avanzado, al no tener tantos enemigos no importa mucho, pero si es un juego con muchos monstruos si importaria...

public class SwordController : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        DemonSlime demonSlime = collision.GetComponent<DemonSlime>();
        MiniDemon miniDemon = collision.GetComponent<MiniDemon>();

        if (demonSlime != null)
        {
            demonSlime.vida--;
            if (demonSlime.vida <= 0)
            {
                collision.GetComponent<Animator>().SetTrigger("Death");
            }
            else
            {
                collision.GetComponent<Animator>().SetTrigger("TakeHit");
            }
        }
        else if (miniDemon != null)
        {
            miniDemon.vida--;
            if (miniDemon.vida <= 0)
            {
                collision.GetComponent<Animator>().SetTrigger("Death");
            }
            else
            {
                collision.GetComponent<Animator>().SetTrigger("TakeHit");
            }
        }
    }
}
}