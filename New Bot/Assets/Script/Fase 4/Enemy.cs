using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyAttr attributes; // receber o ScriptableObject do inimigo

    private Vector2 inDirection; // direção que o player está
    private Vector2 direction,A,B; // 
    public Collider2D target;
    public Animator animator;

    private Vector2 lastPosition;

    public static event Action<Transform> Trans; //EventoSimples 
    public UnityEvent ChangeModeEvent; //Evento Com ScriptableObject

    public bool isWaiting;
    public float waitTime;
    
    


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
        StartCoroutine(SetAnimations());
    }

    // Update is called once per frame
    void Update()
    {

        if (attributes.mode)
        {
            moveTo(target);
        }
        if(isWaiting)
        {
            isWaiting = false;
            StartCoroutine
                (
                    SequenciaCorrotina  // corrotina que garante a execução de corrotinas em sequencia
                        (
                            new List<IEnumerator> // lista de corrotina
                            {
                                Wait(waitTime),
                                attributes.move(transform, animator),
                                ChangeModePlayer()
                            }
                        )
                
                );
            //print("foi");
            //StartCoroutine(attributes.move(transform, animator));
        }
    }

    

    void OnTargetFind(Collider2D col) 
    {
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
        StopCoroutine(SetAnimations());
    }
    // refatorar
    public void moveTo(Collider2D pos)
    {
        target = pos;
        A = new Vector2(transform.position.x, transform.position.y);
        B = new Vector2(target.transform.position.x, target.transform.position.y);

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
        if (transform.position.x != closestPosition.x  || transform.position.y != closestPosition.y )
        {
            attributes.mode = true;
            transform.position = Vector2.MoveTowards(A, closestPosition, attributes.speed * Time.deltaTime);
        }
        else 
        {
            inDirection = (B-A).normalized;
            Debug.Log("foi");
            attributes.mode = false;
            Trans.Invoke(this.transform); // manda o transform position para a câmera. 
            ChangeModeEvent.Invoke(); // informa o player que o modo de jogo mudou.
            animator.SetBool("isMoving", false);
            OnDisable();    // para o inimigo ficar parado apos a interação
            isWaiting = true;   // para começar o contador de 10 segundos
        }
    }
    

    private IEnumerator SetAnimations() // função para ficar atualizando a cada frame a animação do inimigo.
    {
        while (true)
        {
            if ((Vector2)transform.position != lastPosition)
            {
                // Calcula a variação de posição
                direction = (Vector2)transform.position - lastPosition;
                direction = direction.normalized;
                animator.SetFloat("x", direction.x);
                animator.SetFloat("y", direction.y);
                animator.SetBool("isMoving", true);

                // Atualiza a última posição
                lastPosition = transform.position;
                yield return new WaitForEndOfFrameUnit();
            }
            else
            {
                animator.SetBool("isMoving", false);
                yield return new WaitForEndOfFrameUnit();
            }
        }
    }

    private IEnumerator ChangeModePlayer()
    {
        ChangeModeEvent.Invoke();
        yield return null;
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        print("foi");
    }

    //Função para executar uma lista de corrotinas de maneira sequencial e ordenada.
    IEnumerator SequenciaCorrotina(List<IEnumerator> coroutines)
    {
        foreach (var coroutine in coroutines)
        {
            yield return StartCoroutine(coroutine);
        }

    }



}


