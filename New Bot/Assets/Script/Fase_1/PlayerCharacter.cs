using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacter : MonoBehaviour
{
    private GameManager gameManager;
    public float moveSpeed;

    private void Start()
    {
        // Encontra o objeto GameManager
        gameManager = FindObjectOfType<GameManager>();
    
    }

    public void MoveCharacter(int index) //Movimentação
    {
        int movementDirection = -5;
        if (index == 1)
        {
           transform.Translate(Vector3.left * movementDirection * moveSpeed * Time.deltaTime);
        }
        else if (index == 2)
        {
            transform.Translate(Vector3.right * movementDirection * moveSpeed * Time.deltaTime);
        }
        else if (index == 3)
        {
            transform.Translate(Vector3.up * - movementDirection * moveSpeed * Time.deltaTime);
        }
    }
}