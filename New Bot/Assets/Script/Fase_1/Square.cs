using UnityEngine;
using UnityEngine.EventSystems;

public class Plataform : MonoBehaviour, IDropHandler
{
    private GameManager gameManager; // Referência ao GameManager
    
    public void Awake(){

        gameManager = FindObjectOfType<GameManager>(); // Encontra o objeto GameManager na cena

    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            string objetoTag = eventData.pointerDrag.tag;

            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

             switch (objetoTag)
            {
                case "Left":
                    gameManager.numeros.Add(1); // Adiciona o valor 1 à lista
                    break;
                case "Right":
                    gameManager.numeros.Add(2); // Adiciona o valor 2 à lista
                    break;
                case "Up":
                    gameManager.numeros.Add(3); // Adiciona o valor 2 à lista
                    break;

            }

        }

    }

}
