using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Normal, Fuerte, Jefe }
    public EnemyType tipo = EnemyType.Normal;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite normalSprite;
    public Sprite fuerteSprite;
    public Sprite jefeSprite;

    [Header("Efectos")]
    public GameObject dead;
    public GameObject enemyBulletPrefab; // Prefab de la bala enemiga (asignar el objeto "Disparo")

    [Header("Sistema de Disparo")]
    public float shootInterval = 2f; // Intervalo entre disparos
    public int bulletDamage = 10; // Daño de las balas
    
    [Header("Configuración de Profundidad")]
    public float startZ = 10f; // Posición Z inicial (lejos)
    public float endZ = 1f; // Posición Z final (cerca)
    public float zSpeed = 2f; // Velocidad de acercamiento en Z

    public int maxHealth = 3; // Vida base, editable en Inspector
    private int currentHealth;
    private float lastShootTime;

    public bool shouldShoot;
    public float fireRate = 5f;
    private float shotCounter;
    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        // Configurar sprite según el tipo
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
            
        switch (tipo)
        {
            case EnemyType.Normal:
                maxHealth = 3;
                if (normalSprite != null)
                    spriteRenderer.sprite = normalSprite;
                break;
            case EnemyType.Fuerte:
                maxHealth = 10;
                if (fuerteSprite != null)
                    spriteRenderer.sprite = fuerteSprite;
                break;
            case EnemyType.Jefe:
                maxHealth = 30;
                if (jefeSprite != null)
                    spriteRenderer.sprite = jefeSprite;
                break;
        }
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Sistema de disparo unificado
        if (shouldShoot)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                ShootAtPlayer();
                shotCounter = fireRate;
            }
        }
        else
        {
            // Sistema de disparo automático como respaldo
            if (Time.time - lastShootTime >= shootInterval)
            {
                ShootAtPlayer();
                lastShootTime = Time.time;
            }
        }
    }
    
    void ShootAtPlayer()
    {
        if (enemyBulletPrefab != null && PlayerController.instance != null)
        {
            // Calcular dirección hacia el jugador
            Vector3 directionToPlayer = (PlayerController.instance.transform.position - transform.position).normalized;
            
            // Calcular posición inicial del disparo (en el fondo)
            Vector3 bulletStartPos = transform.position;
            bulletStartPos.z = startZ; // Posición Z inicial (lejos)
            
            // Crear el disparo
            GameObject bullet = Instantiate(enemyBulletPrefab, bulletStartPos, Quaternion.identity);
            
            // Configurar el disparo con los parámetros del enemigo
            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
            if (bulletScript != null)
            {
                bulletScript.damageAmount = bulletDamage;
                bulletScript.startZ = startZ;
                bulletScript.endZ = endZ;
                bulletScript.zSpeed = zSpeed;
            }
            
            Debug.Log("Enemigo disparó hacia el jugador. Posición jugador: " + PlayerController.instance.transform.position);
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        
        // Reproducir sonido de daño según el tipo de enemigo
        if (AudioController.instance != null)
        {
            AudioController.instance.PlayEnemyHitSound(tipo);
        }
        
        if(currentHealth <= 0)
        {
            // Reproducir sonido de muerte según el tipo de enemigo
            if (AudioController.instance != null)
            {
                AudioController.instance.PlayEnemyDeathSound(tipo);
            }
            
            Destroy(gameObject);
            // Crea el sprite muerto un poco más abajo que la posición original
            Vector3 deadPosition = transform.position + Vector3.down * 2.5f; // Ajusta el valor 0.5f según necesites
            Instantiate(dead, deadPosition, transform.rotation); 
        }
    }
    
    // Visualizar el firePoint en el editor
    void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, 0.2f);
            Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.up * 1f);
        }
    }
}
