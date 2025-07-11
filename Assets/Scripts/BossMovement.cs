using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private bool startMovingRight = true;
    
    [Header("Configuración Avanzada")]
    [SerializeField] private bool useSmoothMovement = true;
    [SerializeField] private float smoothTime = 0.5f;
    
    private Vector3 startPosition;
    private Vector3 leftBound;
    private Vector3 rightBound;
    private bool movingRight;
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        // Guardar la posición inicial
        startPosition = transform.position;
        
        // Calcular los límites de movimiento
        leftBound = startPosition + Vector3.left * moveDistance;
        rightBound = startPosition + Vector3.right * moveDistance;
        
        // Establecer la dirección inicial
        movingRight = startMovingRight;
    }
    
    void Update()
    {
        MoveBoss();
    }
    
    void MoveBoss()
    {
        Vector3 targetPosition;
        
        if (movingRight)
        {
            targetPosition = rightBound;
        }
        else
        {
            targetPosition = leftBound;
        }
        
        if (useSmoothMovement)
        {
            // Movimiento suave usando SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, moveSpeed);
        }
        else
        {
            // Movimiento directo
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        
        // Cambiar dirección cuando llega a los límites
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }
    
    // Métodos para cambiar parámetros en tiempo de ejecución
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
    
    public void SetMoveDistance(float newDistance)
    {
        moveDistance = newDistance;
        // Recalcular límites
        leftBound = startPosition + Vector3.left * moveDistance;
        rightBound = startPosition + Vector3.right * moveDistance;
    }
    
    public void SetSmoothMovement(bool smooth)
    {
        useSmoothMovement = smooth;
    }
    
    // Método para obtener información de debug
    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            // Dibujar los límites de movimiento
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(leftBound, 0.5f);
            Gizmos.DrawWireSphere(rightBound, 0.5f);
            
            // Dibujar línea de movimiento
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(leftBound, rightBound);
        }
    }
} 