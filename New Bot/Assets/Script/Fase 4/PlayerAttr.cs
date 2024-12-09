using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using static UnityEditor.PlayerSettings;
using System.Drawing;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.RuleTile.TilingRuleOutput;


[CreateAssetMenu(fileName = "Attributes", menuName = "Player/Attributes")]
public class PlayerAttr : ScriptableObject
{
    public float speed;
    public bool mode;
    public Vector2[] points;
    public EnemyAttr.ShapeType shapeType;
    public Vector2 inDirection;
    public int iTriangle, iSquare, i;

    private void OnEnable()
    {
        shapeType = EnemyAttr.ShapeType.None;
        EnemyAttr.TipoForma += SetTipoForma;
        points = null;
    }
    private void OnDisable()
    {
        EnemyAttr.TipoForma -= SetTipoForma;
        points = null;
    }
    public IEnumerator move(UnityEngine.Transform player, Animator anim)
    {
        points = GeneratePoints(shapeType, inDirection);

        while ( (Vector2)player.position != points[i] )
        {
            player.position = Vector2.MoveTowards((Vector2)player.position, points[i], speed * Time.deltaTime);
            yield return new WaitForNextFrameUnit();
        }
        yield return new WaitForNextFrameUnit();
    }

    public Vector2[] GeneratePoints(EnemyAttr.ShapeType type, Vector2 player) // gerando uma lista de pontos baseado no tipo geométrico
    {
        Vector2 right = new Vector2(inDirection.y, -inDirection.x); // Perpendicular à direção
        float size = 1.0f;
        switch (type)
        {
            case EnemyAttr.ShapeType.Square: // tipo quadrado
                points[0] = player + right * size;                       // Canto inferior direito
                points[1] = player + right * size + inDirection * size;   // Canto superior direito
                points[2] = player - right * size + inDirection * size;   // Canto superior esquerdo
                points[3] = player - right * size;                      // Canto inferior esquerdo
                points[4] = player;
                break;

            case EnemyAttr.ShapeType.Triangle: // tipo triangulo
                points = new Vector2[4];
                points[0] = new Vector2(player.x + 1, player.y);
                points[1] = new Vector2(player.x, player.y + 1);
                points[2] = new Vector2(player.x - 1, player.y);
                points[3] = new Vector2(player.x, player.y);
                break;
        }
        return points;
    }
    public void SetTipoForma(EnemyAttr.ShapeType shape)
    {
        shapeType = shape;
    }
    
    public void OnClickSquare()
    {
        if (iSquare <= 4)
        {
            iSquare += 1;
        }
        else
        {
            iSquare = 0;
        }
        
    }
    public void OnClickTriangle()
    {
        if (iSquare <= 3)
        {
            iSquare += 1;
        }
        else
        {
            iSquare = 0;
        }
        
    }
}

