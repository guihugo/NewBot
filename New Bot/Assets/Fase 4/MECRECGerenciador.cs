using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NovoGerenciador", menuName = "MECREC/Gerenciador")]
public class MECRECGerenciador : ScriptableObject
{
    public UnityEvent naAreaDeInteracao;
    public UnityEvent saiuAreaDeInteracao;
    public UnityEvent interagiu;

    public void DispararNaAreaDeInteracao() => naAreaDeInteracao?.Invoke();
    public void DispararSaiuAreaDeInteracao() => saiuAreaDeInteracao?.Invoke();
    public void DispararInteragiu() => interagiu?.Invoke();

}
