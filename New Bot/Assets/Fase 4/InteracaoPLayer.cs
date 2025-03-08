using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracaoPLayer : MonoBehaviour
{
    //public event Action PressE;
    public MECRECGerenciador eventos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            eventos.DispararInteragiu();
        }
    }
}
