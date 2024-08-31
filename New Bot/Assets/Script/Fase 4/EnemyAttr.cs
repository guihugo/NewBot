using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu(fileName = "Attributes", menuName = "Enemy/Attributes")]
public class EnemyAttr : ScriptableObject
{
    public float speed;
    public bool mode;
    public Vector2 inDirection;
    
    private Vector2[] points;

    public enum ShapeType // tipos de formatos geométricos
    {
        Square,
        Triangle
    }
    public ShapeType shapeType;
    public IEnumerator move(Transform enemy, Animator anim)
    {
        points = GeneratePoints(shapeType, enemy);
        foreach (Vector2 pos in points)
        {
            while ((Vector2)enemy.position != pos)
            {
                enemy.position = Vector2.MoveTowards((Vector2)enemy.position, pos, speed * Time.deltaTime);
                yield return new WaitForNextFrameUnit();
            }
            anim.SetFloat("x", inDirection.x);
            anim.SetFloat("y", inDirection.y);
            anim.SetBool("isMoving", true);
            yield return new WaitForSeconds(2);
        }
    }

    public Vector2[] GeneratePoints(ShapeType type, Transform enemy) // gerando uma lista de pontos baseado no tipo geométrico
    {
        switch (type)
        {
            case ShapeType.Square: // tipo quadrado
                points = new Vector2[5];
                points[0] = new Vector2(enemy.position.x+1, enemy.position.y);  // Top-left
                points[1] = new Vector2(enemy.position.x+1, enemy.position.y+2);   // Top-right
                points[2] = new Vector2(enemy.position.x-1, enemy.position.y+2);  // Bottom-right
                points[3] = new Vector2(enemy.position.x-1, enemy.position.y);
                points[4] = new Vector2(enemy.position.x, enemy.position.y);
                break;

            case ShapeType.Triangle: // tipo triangulo
                points = new Vector2[4];
                points[0] = new Vector2(enemy.position.x + 1, enemy.position.y);   // Top
                points[1] = new Vector2(enemy.position.x + 0.5f, enemy.position.y+1); // Bottom-left
                points[2] = new Vector2(enemy.position.x-1, enemy.position.y);
                points[3] = new Vector2(enemy.position.x, enemy.position.y);// Bottom-right
                break;
        }

        return points;
    }
}
