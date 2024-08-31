using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Linq;

public class PlayerFaseQ : MonoBehaviour
{

    public PlayerAttr attr;
    public static event Action<Transform> Trans;
    private Rigidbody2D rig;
    private UnityEngine.Vector2 _playerDirection;
    private Animator animator;


    public int count = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        Trans.Invoke(this.transform);
        attr.mode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attr.mode)
        {
            animator.SetBool("isMoving", false);
            
        }
        else
        {

            _playerDirection = new UnityEngine.Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_playerDirection != Vector2.zero)
            {
                animator.SetFloat("moveX", _playerDirection.x);
                animator.SetFloat("moveY", _playerDirection.y);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(
                    SequenciaCorrotina
                        (
                           new List<IEnumerator> { SetPointsPlayer() }
                        )
                    );
            }
        }
        
        
    }
    void FixedUpdate()
    {
        if (!attr.mode)
        {
            rig.MovePosition(rig.position + _playerDirection * attr.speed * Time.fixedDeltaTime);
        }
    }
    public void ChangeMode()
    {
        attr.mode = !attr.mode;
    }
    public IEnumerator SetPointsPlayer()
    {
        if (count <= attr.points.Length-1)
        {
            attr.points[count] = (Vector2)transform.position;
            count += 1;
        }
        else
        {
            count = 0;

        }
        yield return new WaitForSeconds(1);


    }
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
