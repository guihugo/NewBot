using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerFaseQ : MonoBehaviour
{

    public EnemyAttr attr;
    public static event Action<Transform> Trans;
    private Rigidbody2D rig;
    private UnityEngine.Vector2 _playerDirection;
    private Animator animator;

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
            // Lógica do player para modo de reconhecimento de padrões
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
    
}
