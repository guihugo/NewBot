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

    private Vector2 direction, A, B, lastPosition, inDirection; // 
    public Collider2D target;
    public Animator animator;

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

        if (attributes.mode) // O modo do inimigo é assincro ao do player. Dessa maneira o @mode controla se esta em 'idle' ou perseguição.
        {
            Chase(target);
        }
        if(isWaiting) // @IsWaiting só para controlar quando entra aqui.
        {
            isWaiting = false; // Só quero que aconteça uma vez.
            StartCoroutine // inicio da corrotina
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
        }
        
    }

    // Escutando evento touch
    void OnEnable()
    {
        BoxEvent.touch += Chase;
    }
    private void OnDisable()
    {
        BoxEvent.touch -= Chase;
        StopAllCoroutines();
    }
    // refatorar
    public void Chase(Collider2D pos) // Função para perseguir o player.
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
            attributes.inDirection = inDirection;
            Debug.Log("foi");
            attributes.mode = false;
            Trans.Invoke(this.transform); // manda o transform position para a câmera. 
            ChangeModeEvent.Invoke(); // informa o player que o modo de jogo mudou.
            animator.SetBool("isMoving", false);
            //OnDisable();    // para o inimigo ficar parado apos a interação
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
                direction = ((Vector2)transform.position - lastPosition).normalized;
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
    } // Corrotina para alterar o modo do jogador via evento.

    private IEnumerator Wait(float time) // Corrotina para esperar um @time tempo, em segundos.
    {
        yield return new WaitForSeconds(time);
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


