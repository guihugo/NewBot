using UnityEngine;
using UnityEngine.UI;

public class GetGridOrder : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;

    // Função para obter a ordem de um objeto dentro do GridLayout
    public int GetObjectOrder(GameObject obj)
    {
        // Obter o índice do objeto na lista de filhos
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {
            if (gridLayoutGroup.transform.GetChild(i).gameObject == obj)
            {
                return i; // Retorna o índice do objeto
            }
        }

        // Se o objeto não for encontrado, retorna -1
        return -1;
    }
}
