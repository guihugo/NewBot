using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class BoxEvent : MonoBehaviour
{
    public static event Action<Collider2D> touch;
    public EnemyAttr attr;
    public Boolean check;
    // Start is called before the first frame update
    void Start()
    {
        check = false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "Player" && check == false)
        {   
            check = true;
            print("Foi");
            touch.Invoke(other);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
