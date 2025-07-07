using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    void Start()
    {
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
            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
        }
      }else
      {
        Debug.Log("Missed");
      }
      gunAnim.SetTrigger("Shoot");
    }
  }

  void LateUpdate()
  {
    // Limita la posición del jugador en X y Y
    float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
    float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
    transform.position = new Vector3(clampedX, clampedY, transform.position.z);
  }
}
