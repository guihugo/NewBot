using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F3Player : MonoBehaviour
{
    public GameObject esteira;
    public int speed = 10;
    // Start is called before the first frame update

    private void Awake()
    {
        
        
    }
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.L) && esteira.transform.position.x < 10 )
        {
            esteira.transform.position = new Vector2(esteira.transform.position.x +  speed, esteira.transform.position.y);
        }
        else if(Input.GetKeyDown(KeyCode.J) && esteira.transform.position.x > -10)
        {
            esteira.transform.position = new Vector2(esteira.transform.position.x -  speed, esteira.transform.position.y);
        }
    }

    void FixedUpdate(){
       
    }
}