using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyAttr attributes;
    public float distance;
    public bool check = false;
    public Collider2D target;
    public Animator animator;

    public static event Action<Transform> Trans; //EventoSimples
    public UnityEvent ChangeModeEvent; //Evento Com ScriptableObject

    public float waitTime = 10f;
    public float timer = 0f;
    private bool isWaiting;

    

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        attributes.mode = false;
        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (check && !attributes.mode) // @check informa se esta em perseguição do player; @attributes.mode informa o modo de movimentação
        {
            moveTo(target);
        }
        else
        {
            
            if(isWaiting)
            {
                timer += Time.deltaTime;

                if (timer >= waitTime)
                {
                    isWaiting = false;
                    print("10s");
                    ChangeModeEvent.Invoke();
                }
            }

            //lógica para o inimigo exibir padroes de movimentos.
        }
    }

    

    void OnTargetFind(Collider2D col) 
    {
        Debug.Log(col.gameObject.tag);
        moveTo(col);
    }

    // Escutando evento touch
    void OnEnable()
    {
        BoxEvent.touch += OnTargetFind;
    }
    private void OnDisable()
    {
        BoxEvent.touch -= OnTargetFind;
    }

    public void moveTo(Collider2D pos)
    {
        target = pos;
        Vector2 A = new Vector2(transform.position.x, transform.position.y);
        Vector2 B = new Vector2(target.transform.position.x, target.transform.position.y);
        distance = Vector2.Distance(transform.position, B);

        // Para a animação
        Vector2 direction = (B - A).normalized;
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.SetBool("isMoving", true);

        if (distance > 2)
        {
            check = true;
            
            transform.position = Vector2.MoveTowards(A, B, attributes.speed * Time.deltaTime);
            

        }
        else
        {
            Trans.Invoke(this.transform);
            attributes.mode = true;
            ChangeModeEvent.Invoke();
            animator.SetBool("isMoving", false);
            check = false;  // indicando que já acabou a perseguição
            OnDisable();    // para o inimigo ficar parado apos a interação
            target = GetComponent<BoxCollider2D>(); // para evitar aparecimento de erro
            isWaiting = true;   // para começar o contador de 10 segundos
        }
    }
    
}
