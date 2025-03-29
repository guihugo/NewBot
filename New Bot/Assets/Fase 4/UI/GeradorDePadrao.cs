using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Inventory.UI;
using UnityEngine;
using static MECRECGerenciador;

public class GeradorDePadrao : MonoBehaviour
{
    public GameObject itemPrefab;
    public RectTransform contentPanel;
    public List<GameObject> listOfUItems = new List<GameObject>();
    public int quantidade, quebra;
    private float x = -180, y = 124;

    public MECRECGerenciador gerente; // para requisições do ScriptableObject


    // Start is called before the first frame update
    void Start()
    {
        contentPanel = GetComponent<RectTransform>();
        itemPrefab = gerente.container;
        InitializeInventoryUI(quantidade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeInventoryUI(int inventorySize)
    {
        int contador = 0;
        for (int i = 1; i < inventorySize; i++)
        {
            GameObject uiItem = Instantiate(itemPrefab, contentPanel.gameObject.transform); // Define como filho do contentPanel
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();

            // Ajuste da posição e escala corretamente para UI
            rectTransform.anchoredPosition = new Vector2(x,y);
            rectTransform.localScale = Vector3.one;

            //Selecionando qual deve ser a forma apresentada
            if(Enum.IsDefined(typeof(Formas), gerente.sequenciaCorreta[i-1]))
            {
                string nomeForma = Enum.GetName(typeof(Formas), gerente.sequenciaCorreta[i - 1]);
                Debug.Log(nomeForma);

                GameObject novaForma = Instantiate( gerente.EntregaForma(nomeForma), uiItem.gameObject.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);

                novaForma.GetComponent<Itens>().parentBeforeDrag = uiItem.transform;
                novaForma.transform.SetParent(uiItem.transform, false);

                // Configurar a nova forma dentro do Canvas
                RectTransform novaFormaRect = novaForma.GetComponent<RectTransform>();
                novaFormaRect.SetParent(uiItem.transform.transform.parent, false);
            }
            
            
            
            

            listOfUItems.Add(uiItem);
            x += 180;
            contador++;
            if( i%quebra == 0 )
            {
                contador = 0;
                x = -180;
                y -= 124;
            }
        }
    }

}
