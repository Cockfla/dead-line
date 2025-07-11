using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyWave
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnDelay = 1f;
        public Vector2[] spawnPositions;
    }

    [Header("Configuración de Oleadas")]
    public EnemyWave[] waves;
    public GameObject bossPrefab;
    public Vector2 bossSpawnPosition = new Vector2(0, 8);
    
    [Header("Pantalla de Victoria")]
    public GameObject winScreen;
    public AudioSource winMusic;
    
    private int currentWave = 0;
    private int enemiesRemaining = 0;
    private bool bossSpawned = false;

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWave < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWave]));
        }
        else if (!bossSpawned)
        {
            SpawnBoss();
        }
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        enemiesRemaining = wave.enemyCount;
        
        for (int i = 0; i < wave.enemyCount; i++)
        {
            Vector2 spawnPos = wave.spawnPositions[i % wave.spawnPositions.Length];
            Vector3 spawnPosition = new Vector3(spawnPos.x, spawnPos.y, 10f);
            
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
            
            // Configurar el enemigo para notificar cuando muera
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Crear un evento para cuando el enemigo muera
                StartCoroutine(WaitForEnemyDeath(enemy));
            }
            
            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    IEnumerator WaitForEnemyDeath(GameObject enemy)
    {
        yield return new WaitUntil(() => enemy == null);
        enemiesRemaining--;
        
        if (enemiesRemaining <= 0)
        {
            currentWave++;
            StartNextWave();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        Vector3 bossPosition = new Vector3(bossSpawnPosition.x, bossSpawnPosition.y, 10f);
        GameObject boss = Instantiate(bossPrefab, bossPosition, Quaternion.identity);
        
        // Agregar el componente de movimiento al jefe
        BossMovement bossMovement = boss.GetComponent<BossMovement>();
        if (bossMovement == null)
        {
            bossMovement = boss.AddComponent<BossMovement>();
        }
        
        // Configurar el jefe para notificar cuando muera
        StartCoroutine(WaitForBossDeath(boss));
    }

    IEnumerator WaitForBossDeath(GameObject boss)
    {
        yield return new WaitUntil(() => boss == null);
        WinGame();
    }

    void WinGame()
    {
        Debug.Log("¡Victoria! Has derrotado al jefe final.");
        
        // Mostrar pantalla de victoria
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        
        // Reproducir música de victoria
        if (winMusic != null)
        {
            winMusic.Play();
        }
        
        // Pausar el juego y mostrar el mouse
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
} 