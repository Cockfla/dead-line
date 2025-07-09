using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Normal, Fuerte, Jefe }
    public EnemyType tipo = EnemyType.Normal;

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

    // Start is called before the first frame update
    void Start()
    {
        // Asigna la vida según el tipo de enemigo
        switch (tipo)
        {
            case EnemyType.Normal:
                maxHealth = 3;
                break;
            case EnemyType.Fuerte:
                maxHealth = 10;
                break;
            case EnemyType.Jefe:
                maxHealth = 50;
                break;
        }
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Sistema de disparo automático
        if (Time.time - lastShootTime >= shootInterval)
        {
            ShootAtPlayer();
            lastShootTime = Time.time;
        }
    }
    
    void ShootAtPlayer()
    {
        if (enemyBulletPrefab != null && PlayerController.instance != null)
        {
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
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            // Crea el sprite muerto un poco más abajo que la posición original
            Vector3 deadPosition = transform.position + Vector3.down * 2.5f; // Ajusta el valor 0.5f según necesites
            Instantiate(dead, deadPosition, transform.rotation); 
        }
    }
}
