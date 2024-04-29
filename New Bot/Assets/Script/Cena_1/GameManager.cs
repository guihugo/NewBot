using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool exec = false;
    public List<int> numeros = new List<int>();
    public PlayerCharacter player;

    private void Start()
    {
        // Encontra o objeto PlayerCharacter
        player = FindObjectOfType<PlayerCharacter>();
    }

    public void OnButtonClick()
    {
        player.StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        for (int i = 0; i < numeros.Count; i++)
        {
            Debug.Log("Iniciando");

            int x = numeros[i];
            player.MoveCharacter(x);

            yield return new WaitForSeconds(1);
        }
    }
}
