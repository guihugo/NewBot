using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(int index)
    {
        int movementDirection = -5;
        float gridSize = 0.3f;
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
            transform.Translate(Vector3.up * -movementDirection * gridSize);
        }

        else if (index == 4)
        {
            transform.Translate(Vector3.up * movementDirection * gridSize);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colidiu com uma parede!");
    }
}