using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private GameManager gameManager;
    private int currentIndex = 0;
    public float moveSpeed;

    private void Start()
    {
        // Encontra o objeto GameManager
        gameManager = FindObjectOfType<GameManager>();
    
    }

    private void Update()
    {
        // Verifica se todos os movimentos da lista já foram executados
        if (currentIndex < gameManager.numeros.Count)
        {
            // Obtém o número atual da lista
            int movementDirection = gameManager.numeros[currentIndex];

            // Move o personagem com base no número atual e na velocidade
            MoveCharacter(movementDirection);

            // Incrementa o índice para passar para o próximo número na próxima atualização
            currentIndex++;
        }
    }
    public void MoveCharacter(int movementDirection)
    {
        if (movementDirection > 0)
        {
           transform.Translate(Vector3.right * movementDirection * moveSpeed * Time.deltaTime);
        }
        else if (movementDirection < 0)
        {
            transform.Translate(Vector3.left * movementDirection * moveSpeed * Time.deltaTime);
        }
    }
}
