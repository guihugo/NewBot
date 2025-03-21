using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDropPhase2 : MonoBehaviour
{
    public LayerMask possibleToMoveLayer;
    public float dragOffset = 0.5f;
    public static bool moving;
    private bool isDragging = false;
    private Vector2 originalPosition; 

    private Rigidbody2D rb;
    public CameraLock mainCamera;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        moving = false;
        // Detecta o clique do mouse sobre o personagem
        if (Input.GetMouseButtonDown(0))
        {
            mainCamera.LockCamera();
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {

                isDragging = true;
                originalPosition = rb.position; // Guarda a posi��o original
            }
        }

        // Detecta o movimento do mouse enquanto o bot�o est� pressionado
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(mousePosition + Vector2.up * dragOffset); // Move com um pequeno deslocamento para evitar sobreposi��o
        }

        // Detecta quando o bot�o do mouse � solto
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Verifica se a posi��o final est� em uma �rea v�lida
            if (CanMoveTo(mousePosition))
            {
                moving = true;
                rb.MovePosition(mousePosition);
                mainCamera.UnlockCamera();
            }
            else
            {
                rb.MovePosition(originalPosition); // Retorna � posi��o original se a posi��o final for inv�lida
            }
        }
        

        bool CanMoveTo(Vector2 position)
        {
            // Verifica colis�es com a camada de lugares v�lidos para mover
            Collider2D hitCollider = Physics2D.OverlapCircle(position, 0.1f, possibleToMoveLayer);
            return hitCollider != null;
        }
    }
}
