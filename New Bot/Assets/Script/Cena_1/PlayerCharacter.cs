using UnityEngine;
using UnityEngine.EventSystems;

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
            int i = gameManager.numeros[currentIndex];

            // Move o personagem com base no número atual e na velocidade
            MoveCharacter(i);

            // Incrementa o índice para passar para o próximo número na próxima atualização
            currentIndex++;
        }
    }
    public void MoveCharacter(int index)
    {
        int movementDirection = -1;
        if (index == 1)
        {
           transform.Translate(Vector3.left * movementDirection * moveSpeed * Time.deltaTime);
        }
        else if (index == 2)
        {
            transform.Translate(Vector3.right * - movementDirection * moveSpeed * Time.deltaTime);
        }
        else if (index == 3)
        {
            transform.Translate(Vector3.up * - movementDirection * moveSpeed * Time.deltaTime);
        }
    }
}
