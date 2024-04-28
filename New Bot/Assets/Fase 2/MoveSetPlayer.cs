using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MoveSetPlayer : MonoBehaviour
{
    [SerializeField] private Button botao;
    [SerializeField] private GameObject botaoMap;
    private GameObject Player;
    [SerializeField] private GameObject otherA;
    [SerializeField] private GameObject otherB;
    


    private void Awake()
    {
        botao.onClick.AddListener(OnClickCavalo);
        Player = GameObject.Find("Player");
        
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        botaoMap.transform.position = new Vector2(Player.transform.position.x + 3.5f, Player.transform.position.y);
       
    }

    private void OnClickCavalo()
    {
        if (botaoMap.activeInHierarchy)
        {
            botaoMap.SetActive(false);
        }
        else
        {
            botaoMap.SetActive(true);
            otherA.SetActive(false);
            otherB.SetActive(false);

        }
    }

}
