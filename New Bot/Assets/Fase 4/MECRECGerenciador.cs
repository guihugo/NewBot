using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NovoGerenciador", menuName = "MECREC/Gerenciador")]
public class MECRECGerenciador : ScriptableObject
{

    [SerializeField] public List<int> sequenciaCorreta = new List<int>();
    public UnityEvent naAreaDeInteracao;
    public UnityEvent saiuAreaDeInteracao;
    public UnityEvent interagiu;

    
    //public event Action<GameObject> SendForma;

    public List<GameObject> Itens = new List<GameObject>();
    public GameObject container;
    public Formas formas; // variável para consultas

    
    public enum Formas
    {
        Triangulo = 1,
        Quadrado = 2,
        Circulo = 3,
        Vazio = 4,
    }
    public void DispararNaAreaDeInteracao() => naAreaDeInteracao?.Invoke();
    public void DispararSaiuAreaDeInteracao() => saiuAreaDeInteracao?.Invoke();
    public void DispararInteragiu() => interagiu?.Invoke();

    

    public GameObject EntregaForma(String forma)
    {
        GameObject obj = null;
        if (Enum.TryParse(forma, true, out Formas formaEncontrada))
        {
            obj = Itens[(int)formaEncontrada - 1 ];            
        }
        return obj;
    }
    



}
