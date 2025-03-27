using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeradorDeForma : MonoBehaviour, IPointerClickHandler, IBeginDragHandler
{
    public MECRECGerenciador gerente;
    RectTransform rectTransform;
    //public event Action<String> requestForma;

    [SerializeField] public Forma forma;

    public GameObject objForma;
    public enum Forma
    {
        Triangulo,
        Quadrado,
        Circulo
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        SetForma( gerente.EntregaForma( forma.ToString() ) );
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (objForma == null) return;

        // Criar a nova instância
        GameObject novaForma = Instantiate( objForma , this.rectTransform.anchoredPosition, Quaternion.identity);

        // Configurar a nova forma dentro do Canvas
        RectTransform novaFormaRect = novaForma.GetComponent<RectTransform>();
        novaFormaRect.SetParent(this.transform.parent, false);
        novaFormaRect.anchoredPosition = rectTransform.anchoredPosition;
        novaFormaRect.localScale = Vector3.one;

        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    
    public void SetForma(GameObject obj)
    {
        objForma = obj;
    }

}
