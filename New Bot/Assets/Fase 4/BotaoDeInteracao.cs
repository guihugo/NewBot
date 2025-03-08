using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoDeInteracao : MonoBehaviour
{
    //public event Action naAreaDeInteracao;
    //public event Action SaiuAreaDeInteracao;
    public MECRECGerenciador eventos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.CompareTag("Player") )
        {
            //naAreaDeInteracao.Invoke();
            eventos.DispararNaAreaDeInteracao();
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
