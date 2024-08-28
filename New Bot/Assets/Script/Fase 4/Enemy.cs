using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyAttr attributes;
    public float distance;
    private Vector2 inDirection;
    public Collider2D target;
    public Animator animator;
    public Boolean check = false;

    public static event Action<Transform> Trans; //EventoSimples
    public UnityEvent ChangeModeEvent; //Evento Com ScriptableObject

    public float waitTime = 10f;
    public float timer = 0f;
    private bool isWaiting;


    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        attributes.mode = false;
        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (check)
        {
            moveTo(target);
        }
        else
        {
            if (isWaiting)
            {
                timer += Time.deltaTime;
                if (timer > waitTime)
                {
                    isWaiting = false;
                    //ChangeModeEvent.Invoke();
                    float x = transform.position.x;
                    float y = transform.position.y;
                    StartCoroutine("move", new Vector2(x + 2, y));
                    
                }

            }
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
    // refatorar
    public void moveTo(Collider2D pos)
    {
        target = pos;
        Vector2 A = new Vector2(transform.position.x, transform.position.y);
        Vector2 B = new Vector2(target.transform.position.x, target.transform.position.y);
        distance = Vector2.Distance(A, B);

        // Para a animação

        Vector2 direction = (B - A).normalized;
        inDirection = direction;
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
            check = false;
            ChangeModeEvent.Invoke();
            animator.SetBool("isMoving", false);
            OnDisable();    // para o inimigo ficar parado apos a interação
            isWaiting = true;   // para começar o contador de 10 segundos
        }
    }
    
    private IEnumerator move(Vector2 pos)
    {
        distance = Vector2.Distance(transform.position, pos);
        // Para a animação
        Vector2 direction = (pos - (Vector2)transform.position).normalized;
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.SetBool("isMoving", true);

        while(transform.position.x != pos.x)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, pos, attributes.speed * Time.deltaTime);
            yield return new WaitForNextFrameUnit();
        }
        animator.SetBool("isMoving", false);
        animator.SetFloat("x", inDirection.x);
        animator.SetFloat("y", inDirection.y);
       
    }

    
    
}


