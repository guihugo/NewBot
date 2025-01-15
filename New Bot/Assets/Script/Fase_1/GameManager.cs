using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<int> Numeros = new List<int>(); 
    public PlayerCharacter player;

    private void Start()
    {
        player = FindObjectOfType<PlayerCharacter>();
    }

    public void OnButtonClick()
    {
        player.StartCoroutine(Timer());
    }

    IEnumerator Timer() 
    {
        for (int i = 0; i < Numeros.Count; i++) 
        {
            int x = Numeros[i];
            player.MoveCharacter(x);

            yield return new WaitForSeconds(1);
        }
    }
}
