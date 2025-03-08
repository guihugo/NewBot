using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MECREC : MonoBehaviour
{

    public BotaoDeInteracao BotaoDeInteracao;
    public Transform AreadeInteracao, Menu;
    public InteracaoPLayer InteracaoPLayer;


    private void OnEnable()
    {
        BotaoDeInteracao.naAreaDeInteracao += EntrouAreaDeInteracao;
        BotaoDeInteracao.SaiuAreaDeInteracao += SaiuAreaDeInteracao;
        InteracaoPLayer.PressE += Interagiu;
    }
    private void OnDisable()
    {
        BotaoDeInteracao.naAreaDeInteracao -= EntrouAreaDeInteracao;
        BotaoDeInteracao.SaiuAreaDeInteracao -= SaiuAreaDeInteracao;
        InteracaoPLayer.PressE -= Interagiu;
    }
    // Start is called before the first frame update
    void Start()
    {
        AreadeInteracao = FindDeepChild(transform, "Botao de interacao");
        Menu = FindDeepChild(transform, "Menu Rec");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EntrouAreaDeInteracao()
    {
        
        AreadeInteracao.gameObject.SetActive(true);
    }
    public void SaiuAreaDeInteracao()
    {
        
        AreadeInteracao.gameObject.SetActive(false);
    }

    public void Interagiu()
    {
        AreadeInteracao.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
    }

    //Fun��o para encontrar os filhos de MECREC
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
