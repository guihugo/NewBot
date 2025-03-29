using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MECREC : MonoBehaviour
{
    public MECRECGerenciador gerente;
    public Transform AreaDeInteracao, Menu;
    

    

    private void OnEnable()
    {
        // Eventos para detec��o de aproxima��o do player
        gerente.naAreaDeInteracao.AddListener(EntrouAreaDeInteracao);
        gerente.saiuAreaDeInteracao.AddListener(SaiuAreaDeInteracao);
        gerente.interagiu.AddListener(Interagiu);

    }
    private void OnDisable()
    {
        // Eventos para detec��o de aproxima��o do player
        gerente.naAreaDeInteracao.RemoveListener(EntrouAreaDeInteracao);
        gerente.saiuAreaDeInteracao.RemoveListener(SaiuAreaDeInteracao);
        gerente.interagiu.RemoveListener(Interagiu);
    }
    // Start is called before the first frame update
    void Start()
    {
        AreaDeInteracao = FindDeepChild(transform, "Botao de interacao");
        Menu = FindDeepChild(transform, "Menu Rec");
    }

    

    // Fun��es para 
    public void EntrouAreaDeInteracao()
    {
        
        AreaDeInteracao.gameObject.SetActive(true);
    }
    public void SaiuAreaDeInteracao()
    {
        
        AreaDeInteracao.gameObject.SetActive(false);
    }
    public void Interagiu()
    {
        AreaDeInteracao.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
    }

    //Fun��o para encontrar os filhos de MECREC que � um encapsulador.(Filhos s�o os outros gameObject dentro de MECREC)
    Transform FindDeepChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform found = FindDeepChild(child, name);
            if (found != null)
                return found;
        }
        return null;
    }
}
