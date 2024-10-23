using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plataform : MonoBehaviour, IDropHandler
{
    private GameManager gameManager;
    private RectTransform rectTransform;
    //public GridLayoutGroup gridLayoutGroup;

    public GameObject anchoredGameObj;

    private CanvasGroup canvasGroup;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && anchoredGameObj == null)
        {
            // Cria uma duplicata do objeto arrastado e a ancora ao componente de recepção de comandos.
            anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
            anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
            anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
            anchoredGameObj.GetComponent<Direction>().Anchor(this.gameObject);

            // Adiciona à fila de ações uma posição após a posição atual na fila.
            //GameObject.Find("PainelAcoes").GetComponent<PainelAcoes>().Add(queuePosition + 1);

            // Adiciona à fila de arrasto o objeto ancorado e a posição atual na fila.
            //GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(anchoredGameObj, queuePosition);
        }
    }
}
