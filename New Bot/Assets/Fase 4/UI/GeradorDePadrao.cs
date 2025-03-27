using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

public class GeradorDePadrao : MonoBehaviour
{
    public GameObject itemPrefab;
    public RectTransform contentPanel;
    public List<GameObject> listOfUItems = new List<GameObject>();
    public int cont;
    private float x = -180, y = 124;

    public MECRECGerenciador gerente; // para requisições do ScriptableObject


    // Start is called before the first frame update
    void Start()
    {
        contentPanel = GetComponent<RectTransform>();
        InitializeInventoryUI(cont);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 1; i < inventorySize; i++)
        {
            GameObject uiItem = Instantiate(itemPrefab, contentPanel.gameObject.transform); // Define como filho do contentPanel
            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();

            // Ajuste da posição e escala corretamente para UI
            rectTransform.anchoredPosition = new Vector2(x,y);
            rectTransform.localScale = Vector3.one;


            listOfUItems.Add(uiItem);
            x += 180;
            if( i%3 == 0 )
            {
                x = -180;
                y -= 124;
            }
        }
    }

}
