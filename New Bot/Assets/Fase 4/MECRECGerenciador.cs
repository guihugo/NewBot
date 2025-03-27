using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NovoGerenciador", menuName = "MECREC/Gerenciador")]
public class MECRECGerenciador : ScriptableObject
{

    public int sequencia;
    public UnityEvent naAreaDeInteracao;
    public UnityEvent saiuAreaDeInteracao;
    public UnityEvent interagiu;

    
    //public event Action<GameObject> SendForma;

    public List<GameObject> Itens = new List<GameObject>();

    
    public enum Formas
    {
        Triangulo = 0,
        Quadrado = 1,
        Circulo = 2
    }
    public void DispararNaAreaDeInteracao() => naAreaDeInteracao?.Invoke();
    public void DispararSaiuAreaDeInteracao() => saiuAreaDeInteracao?.Invoke();
    public void DispararInteragiu() => interagiu?.Invoke();

    

    public GameObject EntregaForma(String forma)
    {
        GameObject obj = null;
        if (Enum.TryParse(forma, true, out Formas formaEncontrada))
        {
            Debug.Log(formaEncontrada);
            obj = Itens[(int)formaEncontrada];
            
        }
        return obj;
    }
    



}
