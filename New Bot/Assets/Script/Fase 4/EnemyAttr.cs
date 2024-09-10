using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[CreateAssetMenu(fileName = "Attributes", menuName = "Enemy/Attributes")]
public class EnemyAttr : ScriptableObject
{
    public float speed;
    public bool mode;
    public Vector2 inDirection;
    public static event Action<ShapeType> TipoForma;
    public Vector2[] points, normalized;

    public enum ShapeType // tipos de formatos geométricos
    {
        None,
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
        TipoForma.Invoke(shapeType);
        switch (type)
        {
            case ShapeType.Square: // tipo quadrado
                points = new Vector2[5];
                Vector2 right = new Vector2(inDirection.y, -inDirection.x); // Perpendicular à direção
                float size = 1.0f;
                /**   
                points[0] = new Vector2(enemy.position.x + right.x + 1, enemy.position.y + right.y);  
                points[1] = new Vector2(enemy.position.x+1, enemy.position.y+2);   
                points[2] = new Vector2(enemy.position.x-1, enemy.position.y+2);  
                points[3] = new Vector2(enemy.position.x-1, enemy.position.y);
                points[4] = new Vector2(enemy.position.x, enemy.position.y); */
                //gpt code
                points[0] = (Vector2)enemy.position + right * size;                       // Canto inferior direito
                points[1] = (Vector2)enemy.position + right * size + inDirection * size;   // Canto superior direito
                points[2] = (Vector2)enemy.position - right * size + inDirection * size;   // Canto superior esquerdo
                points[3] = (Vector2)enemy.position - right * size;                      // Canto inferior esquerdo
                points[4] = enemy.position;

                break;

            case ShapeType.Triangle: // tipo triangulo
                points = new Vector2[4];
                points[0] = new Vector2(enemy.position.x + 1, enemy.position.y);  
                points[1] = new Vector2(enemy.position.x, enemy.position.y+1); 
                points[2] = new Vector2(enemy.position.x-1, enemy.position.y);
                points[3] = new Vector2(enemy.position.x, enemy.position.y);
                break;
        }
        normalized = new Vector2[points.Length];
        Array.Copy(points, normalized, points.Length);
        NormalizesPoints(normalized);
        return points;
    }
    public Vector2[] NormalizesPoints(Vector2[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = points[i].normalized;
        }
        return points;
    }
    public void OnDisable()
    {
        inDirection = new Vector2();
        points = null;
        normalized = null;
    }
    public void OnEnable()
    {
        inDirection = new Vector2();
        points = null;
        normalized = null;
    }
}
