using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        //[SerializeField]
        //private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        //private int currentlyDraggedItemIndex = -1;

        //public event Action<int>  OnStartDragging;

        //public event Action<int, int> OnSwapItems;

        private GameObject anchoredGameObj;

        private RectTransform rectTransform;

        public Sprite image;
        public int quantity;

        private void Awake()
        {
            //mouseFollower.Toggle(false);
            InitializeInventoryUI(3);
        }

        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, contentPanel); 
                uiItem.transform.localScale = Vector3.one; 
                uiItem.transform.localPosition = Vector3.zero; 

                RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
                rectTransform.anchoredPosition3D = Vector3.zero;
                rectTransform.localScale = Vector3.one;

                listOfUIItems.Add(uiItem);
                
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            // Verifica se o ponteiro arrastado não é nulo e se não há um objeto ancorado neste componente.
            if (eventData.pointerDrag != null && anchoredGameObj == null)
            {
                // Cria uma duplicata do objeto arrastado e a ancora ao componente de recepção de comandos.
                anchoredGameObj = Instantiate(eventData.pointerDrag, this.gameObject.transform, true);
                anchoredGameObj.GetComponent<RectTransform>().position = rectTransform.position;
                anchoredGameObj.GetComponent<RectTransform>().rotation = rectTransform.rotation;
                anchoredGameObj.GetComponent<RectTransform>().localScale = rectTransform.localScale;
                //anchoredGameObj.GetComponent<Direction>().Anchor(this.gameObject);

                // Adiciona à fila de ações uma posição após a posição atual na fila.
                //GameObject.Find("PainelAcoes").GetComponent<PainelAcoes>().Add(queuePosition + 1);

                // Adiciona à fila de arrasto o objeto ancorado e a posição atual na fila.
                //GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(anchoredGameObj, queuePosition);
            }
        }
    }
}