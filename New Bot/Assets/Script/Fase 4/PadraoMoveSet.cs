using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
[CreateAssetMenu(fileName ="PadraoMoveSet", menuName ="Enemy/PMoveSet")]
public class PadraoMoveSet : ScriptableObject
{
    [SerializeField] Vector2[] points;
    [SerializeField] Animator animator;
    [SerializeField] Vector2 direction;
    

    private IEnumerator move(Vector2[] points)
    {
        foreach (Vector2 pos in points)
        {
            // Para a animação
            //direction = (pos - (Vector2)transform.position).normalized;
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
            animator.SetBool("isMoving", true);

           // while (transform.position.x != pos.x)
            {
               // transform.position = Vector2.MoveTowards((Vector2)transform.position, pos, attributes.speed * Time.deltaTime);
                yield return new WaitForNextFrameUnit();
            }
            
            yield return new WaitForSeconds(2);
        }
    }

}
