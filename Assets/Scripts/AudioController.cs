using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    
    public AudioSource gunAudio, enemyAudio, playerHurt;

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

    public void PlayEnemySound()
    {
        enemyAudio.Stop();
        enemyAudio.Play();
    }

    public void PlayPlayerHurtSound()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }

    
}
