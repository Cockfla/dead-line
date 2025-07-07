using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { Normal, Fuerte, Jefe }
    public EnemyType tipo = EnemyType.Normal;

    public int maxHealth = 3; // Vida base, editable en Inspector
    private int currentHealth;

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
        
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
