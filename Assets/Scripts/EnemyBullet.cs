using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public int damageAmount = 10;

    [Header("Sistema de Profundidad 3D Simulado")]
    public float startZ = 10f; // Posición Z inicial (lejos)
    public float endZ = 1f; // Posición Z final (cerca)
    public float zSpeed = 2f; // Velocidad de acercamiento en Z
    public float currentZ; // Posición Z actual
    
    [Header("Configuración de Colisión")]
    public float impactRadius = 1f; // Radio de impacto cuando está cerca
    public float maxScale = 3f; // Escala máxima cuando está cerca
    public float minScale = 0.1f; // Escala inicial cuando está lejos
    
    [Header("Configuración de Vida")]
    public float lifetime = 15f; // Tiempo de vida en segundos

    private Vector3 initialScale;
    private float timeAlive = 0f;
    private bool hasHit = false;
    private Vector3 targetPosition; // Posición objetivo en 2D

    // Start is called before the first frame update
    void Start()
    {
        // Verificar que el PlayerController exista
        if (PlayerController.instance == null)
        {
            Debug.LogError("EnemyBullet: No se encontró PlayerController.instance!");
            return;
        }
        
        // Inicializar posición Z
        currentZ = startZ;
        
        // Calcular posición objetivo (donde está el jugador)
        targetPosition = PlayerController.instance.transform.position;
        
        // Inicializar escala
        initialScale = transform.localScale;
        transform.localScale = initialScale * minScale;
        
        // Posicionar la bala en el fondo (Z lejos)
        Vector3 startPos = transform.position;
        startPos.z = currentZ;
        transform.position = startPos;
        
        Debug.Log("EnemyBullet 3D creada. Z inicial: " + currentZ + " - Objetivo: " + targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHit) return;
        
        // Incrementar tiempo de vida
        timeAlive += Time.deltaTime;
        
        // Destruir si excede el tiempo de vida
        if (timeAlive >= lifetime)
        {
            StartCoroutine(DestroyBullet());
            return;
        }
        
        // Actualizar posición objetivo del jugador en tiempo real
        if (PlayerController.instance != null)
        {
            targetPosition = PlayerController.instance.transform.position;
        }
        
        // Simular movimiento en Z (profundidad)
        currentZ -= zSpeed * Time.deltaTime;
        
        // Calcular escala basada en la posición Z
        float zProgress = Mathf.InverseLerp(startZ, endZ, currentZ);
        float currentScale = Mathf.Lerp(minScale, maxScale, zProgress);
        transform.localScale = initialScale * currentScale;
        
        // Mover la bala hacia la posición objetivo en 2D (actualizada en tiempo real)
        Vector3 currentPos = transform.position;
        Vector3 direction = (targetPosition - currentPos).normalized;
        float moveSpeed = zSpeed * (1f + zProgress); // Más rápido cuando está cerca
        currentPos += direction * moveSpeed * Time.deltaTime;
        currentPos.z = currentZ; // Mantener la Z actual
        transform.position = currentPos;
        
        // Verificar colisión cuando está cerca
        if (currentZ <= endZ)
        {
            CheckImpact();
        }
    }
    
    void CheckImpact()
    {
        if (hasHit || PlayerController.instance == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        
        if (distanceToPlayer <= impactRadius)
        {
            hasHit = true;
            Debug.Log("¡Impacto 3D en el jugador! Daño: " + damageAmount + " - Distancia: " + distanceToPlayer);
            PlayerController.instance.TakeDamage(damageAmount);
            StartCoroutine(DestroyBullet());
        }
        else
        {
            // Si no impactó, destruir la bala
            StartCoroutine(DestroyBullet());
        }
    }

    private IEnumerator DestroyBullet()
    {
        // Desactivar el collider para evitar más colisiones
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        
        // Esperar un frame antes de destruir
        yield return null;
        
        // Destruir el objeto de forma segura
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
