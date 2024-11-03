using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{

    public class UIInventoryItem : MonoBehaviour, IDropHandler
    {
        private GameManager gameManager;

        public GameObject anchoredGameObj;

        private RectTransform rectTransform;

        private CanvasGroup canvasGroup;

        private UIInventoryPage inventoryPage;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            gameManager = FindObjectOfType<GameManager>();
            inventoryPage = FindObjectOfType<UIInventoryPage>();
        }
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Drop");
            if (eventData.pointerDrag != null && anchoredGameObj == null)
            {
                anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
                anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
                anchoredGameObj.GetComponent<Direction>().Anchor(this.gameObject);

                int directionValue = anchoredGameObj.GetComponent<Direction>().direction;

                inventoryPage.InitializeInventoryUI(1);

                gameManager.Numeros.Add(directionValue);

                // Adiciona à fila de ações uma posição após a posição atual na fila.
                //GameObject.Find("PainelAcoes").GetComponent<PainelAcoes>().Add(queuePosition + 1);

                // Adiciona à fila de arrasto o objeto ancorado e a posição atual na fila.
                //GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(anchoredGameObj, queuePosition);
            }
        }
    }
}