using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    [Header("Sonidos del Jugador")]
    public AudioSource gunAudio, playerHurt;
    
    [Header("Sonidos de Enemigos - Daño")]
    public AudioSource enemyNormalHit;
    public AudioSource enemyFuerteHit;
    public AudioSource enemyJefeHit;
    
    [Header("Sonidos de Enemigos - Muerte")]
    public AudioSource enemyNormalDeath;
    public AudioSource enemyFuerteDeath;
    public AudioSource enemyJefeDeath;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGunSound()
    {
        gunAudio.Stop();
        gunAudio.Play();
    }

    public void PlayPlayerHurtSound()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }
    
    // Métodos para sonidos de daño por tipo de enemigo
    public void PlayEnemyHitSound(EnemyController.EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyController.EnemyType.Normal:
                if (enemyNormalHit != null)
                {
                    enemyNormalHit.Stop();
                    enemyNormalHit.Play();
                }
                break;
            case EnemyController.EnemyType.Fuerte:
                if (enemyFuerteHit != null)
                {
                    enemyFuerteHit.Stop();
                    enemyFuerteHit.Play();
                }
                break;
            case EnemyController.EnemyType.Jefe:
                if (enemyJefeHit != null)
                {
                    enemyJefeHit.Stop();
                    enemyJefeHit.Play();
                }
                break;
        }
    }
    
    // Métodos para sonidos de muerte por tipo de enemigo
    public void PlayEnemyDeathSound(EnemyController.EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyController.EnemyType.Normal:
                if (enemyNormalDeath != null)
                {
                    enemyNormalDeath.Stop();
                    enemyNormalDeath.Play();
                }
                break;
            case EnemyController.EnemyType.Fuerte:
                if (enemyFuerteDeath != null)
                {
                    enemyFuerteDeath.Stop();
                    enemyFuerteDeath.Play();
                }
                break;
            case EnemyController.EnemyType.Jefe:
                if (enemyJefeDeath != null)
                {
                    enemyJefeDeath.Stop();
                    enemyJefeDeath.Play();
                }
                break;
        }
    }
    
    // Método de compatibilidad para mantener el código existente
    public void PlayEnemySound()
    {
        // Por defecto reproduce el sonido de enemigo normal
        PlayEnemyHitSound(EnemyController.EnemyType.Normal);
    }
}
