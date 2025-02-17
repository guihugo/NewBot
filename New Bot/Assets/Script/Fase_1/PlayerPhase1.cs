using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPhase1 : MonoBehaviour
{
    private Rigidbody2D rb;

    public int movementDirection;
    public float gridSize;
    private bool isMoving = false;
    public float moveSpeed = 5f;

    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveCharacter(int index)
    {
        if (!isMoving)
        {
            Vector3 moveVector = Vector3.zero;

            if (index == 1)
            {
                moveVector = Vector3.left * movementDirection;
            }
            else if (index == 2)
            {
                moveVector = Vector3.right * movementDirection;
            }
            else if (index == 3)
            {
                moveVector = Vector3.down * movementDirection;
            }
            else if (index == 4)
            {
                moveVector = Vector3.up * movementDirection;
            }

            if (moveVector != Vector3.zero)
            {
                StartCoroutine(MoveSmoothly(moveVector));
            }
        }
    }

    private IEnumerator MoveSmoothly(Vector3 direction)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + direction * gridSize;

        float elapsedTime = 0f;
        float duration = gridSize / moveSpeed;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        StopAllCoroutines();
        isMoving = false;
    }
}