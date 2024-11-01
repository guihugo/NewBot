using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool exec = false; //Check Botão
    public List<int> numeros = new List<int>(); //Lista de inteiro
    public PlayerCharacter player;

    private void Start()
    {
        // Encontra o objeto PlayerCharacter
        player = FindObjectOfType<PlayerCharacter>();
    }

    public void OnButtonClick() //Botão
    {
        player.StartCoroutine(timer()); //Start Corroutine
    }

    IEnumerator timer() //Corroutine
    {
        for (int i = 0; i < numeros.Count; i++) //For para percorrer a lista
        {
            Debug.Log("Iniciando");

            int x = numeros[i];
            player.MoveCharacter(x); //Função movimento

            yield return new WaitForSeconds(1); //Espere
        }
    }
}
