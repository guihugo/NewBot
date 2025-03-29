using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracaoPLayer : MonoBehaviour
{
    //public event Action PressE;
    public MECRECGerenciador eventos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            eventos.DispararNaAreaDeInteracao();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E))
        {
            eventos.DispararInteragiu();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SaiuAreaDeInteracao.Invoke();
            eventos.DispararSaiuAreaDeInteracao();
        }
    }
}
