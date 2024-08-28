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
    private Vector2 inDirection;
    private Vector2 direction;
    public Collider2D target;
    public Animator animator;

    public static event Action<Transform> Trans; //EventoSimples 
    public UnityEvent ChangeModeEvent; //Evento Com ScriptableObject

    public float waitTime = 10f;
    public float timer = 0f;
    private bool isWaiting;

    public Vector2[] pointsP;
    


    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        attributes.mode = false;
        pointsP = new Vector2[3];
        float x = transform.position.x;
        float y = transform.position.y;
        isWaiting = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (attributes.mode)
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
                    pointsP[0] = new Vector2(transform.position.x + 2, transform.position.y);
                    pointsP[1] = new Vector2(transform.position.x - 2, transform.position.y);
                    pointsP[2] = new Vector2(transform.position.x, transform.position.y);

                    StartCoroutine("move", pointsP);
                    
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

        // Posições candidatas para o inimigo parar (gpt code)
        Vector2[] stopPositions = new Vector2[]
        {
            new Vector2(B.x + 2, B.y), // à direita
            new Vector2(B.x - 2, B.y), // à esquerda
            new Vector2(B.x, B.y + 2), // acima
            new Vector2(B.x, B.y - 2)  // abaixo
        };

        // Variável para armazenar a posição mais próxima
        Vector2 closestPosition = stopPositions[0];
        float closestDistance = Vector2.Distance(transform.position, stopPositions[0]);

        // Encontrar a posição mais próxima
        for (int i = 1; i < stopPositions.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, stopPositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = stopPositions[i];
            }
        }
        // Para a animação

        direction = (B-A).normalized;
        inDirection = direction;
        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
        animator.SetBool("isMoving", true);

        if (transform.position.x != closestPosition.x || transform.position.y != closestPosition.y)
        {
            attributes.mode = true;
            transform.position = Vector2.MoveTowards(A, closestPosition, attributes.speed * Time.deltaTime);
        }
        else 
        {
            Trans.Invoke(this.transform); // manda o transform position para a câmera. 
            attributes.mode = false;
            ChangeModeEvent.Invoke();
            animator.SetBool("isMoving", false);
            OnDisable();    // para o inimigo ficar parado apos a interação
            isWaiting = true;   // para começar o contador de 10 segundos
        }
    }
    
    private IEnumerator move(Vector2[] points)
    {
        foreach ( Vector2 pos in points){
            // Para a animação
            direction = (pos - (Vector2)transform.position).normalized;
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
            animator.SetBool("isMoving", true);

            while (transform.position.x != pos.x)
            {
                transform.position = Vector2.MoveTowards((Vector2)transform.position, pos, attributes.speed * Time.deltaTime);
                yield return new WaitForNextFrameUnit();
            }
            animator.SetBool("isMoving", false);
            animator.SetFloat("x", inDirection.x);
            animator.SetFloat("y", inDirection.y);
            yield return new WaitForSeconds(2);
        }
    }

    
    
}


