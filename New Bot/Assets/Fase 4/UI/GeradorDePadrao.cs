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
    public float x = -180, y = 124;

    public MECRECGerenciador gerente; // para requisições do ScriptableObject


    // Start is called before the first frame update
    void Start()
    {
        contentPanel = GetComponent<RectTransform>();
        itemPrefab = gerente.container;
        InitializeInventoryUI(quantidade);
    }

   
    public void InitializeInventoryUI(int inventorySize)
    {
        int contador = 0;
        for (int i = 1; i < inventorySize; i++)
        {
            GameObject uiItem = Instantiate(itemPrefab, contentPanel.gameObject.transform); // Define como filho do contentPanel
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            uiItem.GetComponent<DropContainer>().index = i-1;
            Debug.Log(i - 1);
            // Ajuste da posição e escala corretamente para UI
            rectTransform.anchoredPosition = new Vector2(x,y);
            rectTransform.localScale = Vector3.one;

            //Selecionando qual deve ser a forma apresentada
            if( Enum.IsDefined(typeof(Formas), gerente.sequenciaInicial[i-1]) && !(gerente.sequenciaInicial[i-1] == 4) )
            {
                string nomeForma = Enum.GetName(typeof(Formas), gerente.sequenciaInicial[i - 1]);

                GameObject novaForma = Instantiate( gerente.EntregaForma(nomeForma), uiItem.gameObject.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);

                novaForma.GetComponent<Itens>().parentBeforeDrag = uiItem.transform;
                novaForma.GetComponent<Itens>().locked = true;                                     // Item não pode ser movido, está travado            
                novaForma.gameObject.transform.SetParent(uiItem.gameObject.transform, false);

                // Configurar a nova forma dentro do Canvas
                RectTransform novaFormaRect = novaForma.GetComponent<RectTransform>();
                novaFormaRect.SetParent(uiItem.transform.transform.parent, false);
                uiItem.GetComponent<DropContainer>().isAchorned = true;                             // Container está alocado
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
