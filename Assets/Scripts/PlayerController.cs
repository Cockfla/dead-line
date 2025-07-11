using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public Rigidbody2D theRB;
    public float moveSpeed = 5.0f;
      private float moveInput;
    private Vector2 mouseInput;

    public float mouseSensitivity = 1.0f;

    public Camera viewCam;

    public GameObject bulletImpact;

    public float minY = -8.34f; // Límite inferior
    public float maxY = 4.0f; // Límite superior
    public float minX = -34.51f; // Límite inferior
    public float maxX = 35.03f; // Límite superior

    public Animator gunAnim;
    public Animator anim;

    public int currentHealth ;
    public int maxHealth = 100;
    public GameObject deadScreen;
    private bool hasDied;

    public TextMeshProUGUI healthText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString() + "%";
        // If viewCam is not assigned, try to find the main camera
        if (viewCam == null)
        {
            viewCam = Camera.main;
            if (viewCam == null)
            {
                viewCam = FindObjectOfType<Camera>();
            }
        }
    }

  void Update()
  {
    // No procesar input si el juego está pausado o el jugador está muerto
    if(Time.timeScale == 0f || hasDied)
    {
      // Si está muerto o pausado, detener todo movimiento y input
      theRB.velocity = Vector2.zero;
      anim.SetBool("isMoving", false);
      return;
    }
    
    //player movement
    moveInput = Input.GetAxis("Horizontal");

    theRB.velocity = new Vector2(moveInput * moveSpeed, 0);

    //player view control
    mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

    // Movimiento vertical con el mouse (limitado)
    float newY = Mathf.Clamp(transform.position.y + mouseInput.y, minY, maxY);
    transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    //shooting
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        Debug.Log("Hit: " + hit.collider.name);
        Instantiate(bulletImpact, hit.point, transform.rotation);

        if(hit.transform.tag == "Enemy")
        {
            // El sonido de daño ahora se reproduce dentro del TakeDamage() del enemigo
            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
        }
        
      }else
      {
        Debug.Log("Missed");
      }
      AudioController.instance.PlayGunSound();
      gunAnim.SetTrigger("Shoot");
    }
    if(moveInput != 0f)
    {
      anim.SetBool("isMoving", true);
    }
    else
    {
      anim.SetBool("isMoving", false);
    }
  }

  void LateUpdate()
  {
    // Limita la posición del jugador en X y Y
    float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
    float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
  }

  public void TakeDamage(int damageAmount)
  {
    AudioController.instance.PlayPlayerHurtSound();
    currentHealth -= damageAmount;
    if(currentHealth <= 0)
    {
      deadScreen.SetActive(true);
      hasDied = true;
      currentHealth = 0;
      
      // Pausar el juego y mostrar el mouse
      Time.timeScale = 0f;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }

    healthText.text = currentHealth.ToString() + "%";
  }

  public void AddHealth(int healthAmount)
  {
    currentHealth += healthAmount;
    if(currentHealth > maxHealth)
    {
      currentHealth = maxHealth;
    }
  }
  
  // Método para mostrar información de debug
  void OnGUI()
  {
    if (!hasDied)
    {
      GUI.Label(new Rect(10, 10, 200, 20), "Vida: " + currentHealth + "/" + maxHealth);
      GUI.Label(new Rect(10, 30, 200, 20), "Posición: " + transform.position.ToString("F2"));
    }
  }
}
