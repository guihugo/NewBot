using UnityEngine;
using UnityEngine.EventSystems;

public class Plataform : MonoBehaviour, IDropHandler
{
    private GameManager gameManager;
    
    public void Awake(){

        gameManager = FindObjectOfType<GameManager>();

    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            string objetoTag = eventData.pointerDrag.tag;

            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; PUSH

             switch (objetoTag)
            {
                case "Left":
                    gameManager.Numeros.Add(1); 
                    break;
                case "Right":
                    gameManager.Numeros.Add(2); 
                    break;
                case "Up":
                    gameManager.Numeros.Add(3);
                    break;

            }

        }

    }

}
