using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public Text winText;  // Arraste o Text da UI para este campo no Inspector

    private void Start()
    {
        winText.gameObject.SetActive(false);  // Oculta o texto inicialmente
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("check");
            winText.gameObject.SetActive(true);  // Exibe o texto "Você ganhou"
            winText.text = "Você ganhou!";

        }
    }
}
