using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraLock : MonoBehaviour
{
    public bool isCameraLocked = false; // Vari�vel que controla o estado da c�mera
    private Vector3 lockedPosition;
    public Transform player;         // Refer�ncia ao transform do jogador
    public float smoothSpeed = 0.005f;  // Velocidade de suaviza��o (quanto menor, mais suave)

    void Start()
    {
        // Armazena a posi��o inicial da c�mera (ou a posi��o atual ao travar)
        lockedPosition = transform.position;
    }

    void Update()
    {
        if (isCameraLocked)
        {
            // Trava a posi��o da c�mera na posi��o armazenada
            transform.position = lockedPosition;
        }
        else
        {
           
            HandleCameraMovement();
        }
    }

    void HandleCameraMovement()
    {
        // Exemplo simples de movimenta��o da c�mera (ajuste conforme necess�rio)


        Vector3 smoothedPosition = Vector3.Lerp( transform.position, player.position , smoothSpeed );
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

    public void LockCamera()
    {
        isCameraLocked = true;
        lockedPosition = transform.position; // Armazena a posi��o no momento do bloqueio
    }

    public void UnlockCamera()
    {
        isCameraLocked = false;
    }
    
}

