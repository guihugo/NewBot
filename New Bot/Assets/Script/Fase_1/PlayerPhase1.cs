using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPhase1 : MonoBehaviour
{
    private Rigidbody2D rb;

    public int movementDirection;
    public float gridSize;
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(int index)
    {
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
    }
}