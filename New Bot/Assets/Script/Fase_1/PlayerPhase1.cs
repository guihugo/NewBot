using System.Collections;
using UnityEngine;

public class PlayerPhase1 : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movimento")]
    public float gridSize = 0.5f;
    public float moveSpeed = 5f;

    [Header("Colisão")]
    [SerializeField] private LayerMask obstacleLayer;

    public bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public IEnumerator MoveCharacter(int index)
    {
        if (isMoving)
            yield break;

        Vector2 direction = Vector2.zero;

        switch (index)
        {
            case 1: direction = Vector2.left; break;
            case 2: direction = Vector2.right; break;
            case 3: direction = Vector2.down; break;
            case 4: direction = Vector2.up; break;
        }

        if (direction == Vector2.zero)
            yield break;

        if (!CanMove(direction))
        {
            Debug.Log("Movimento bloqueado");
            yield break;
        }

        yield return StartCoroutine(MoveSmoothly(direction));
    }

    private bool CanMove(Vector2 direction)
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        
        RaycastHit2D hit = Physics2D.BoxCast(
            box.bounds.center,
            box.bounds.size * 0.9f,
            0f,
            direction,
            gridSize,
            obstacleLayer);

        Debug.Log(hit.collider);

        return hit.collider == null;
    }

    private IEnumerator MoveSmoothly(Vector2 direction)
    {
        isMoving = true;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + direction * gridSize;

        float duration = gridSize / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }
}