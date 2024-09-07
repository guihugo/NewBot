using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacter : MonoBehaviour
{
    private GameManager gameManager;
    public float moveSpeed;
    public float gridSize = 1f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void MoveCharacter(int index)
    {
        int movementDirection = -5;
        if (index == 1)
        {
           transform.Translate(Vector3.left * movementDirection * gridSize);
        }
        else if (index == 2)
        {
            transform.Translate(Vector3.right * movementDirection * gridSize);
        }
        else if (index == 3)
        {
            transform.Translate(Vector3.up * - movementDirection * gridSize);
        }
    }
}