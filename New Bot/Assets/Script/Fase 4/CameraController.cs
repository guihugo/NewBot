using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerAttr attr;
    private Vector3 lockedPosition;
    public Transform player;
    public Transform enemy;
    public float smoothSpeed = 0.005f;  // Velocidade de suavização (quanto menor, mais suave)

    public Vector3 desiredPosition ;

    void Start()
    {
        
        // Armazena a posição inicial da câmera (ou a posição atual ao travar)
        lockedPosition = transform.position;
    }
    void setPlayerTransform(Transform t)
    {
        player = t;
    }
    void setEnemyTransform(Transform t)
    {
        enemy = t;
    }
    
    void OnEnable()
    {
        PlayerFaseQ.Trans += setPlayerTransform;
        Enemy.Trans += setEnemyTransform;
    }
    void OnDisable()
    {
        PlayerFaseQ.Trans -= setPlayerTransform;
        Enemy.Trans -= setEnemyTransform;
    }
    void Update()
    {
        
        if (attr.mode)
        {
            HandleCameraMovement(desiredPosition);
        }
        else
        {

            HandleCameraMovement(player.position);
        }
    }

    void HandleCameraMovement(Vector3 desiredPosition)
    {
        // Exemplo simples de movimentação da câmera (ajuste conforme necessário)


        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

    public void LockCamera()
    {
        desiredPosition = (enemy.position + player.position)/2; // Armazena a posição no momento do bloqueio
    }

    public void UnlockCamera()
    {
        attr.mode = false;
    }
}
