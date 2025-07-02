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

    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

    viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(-mouseInput.y, 0f));

    //shooting
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = viewCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        Debug.Log("Hit: " + hit.collider.name);
        // Here you can add logic to handle what happens when the player shoots an object
        // For example, you could apply damage to the object or destroy it
      }else
      {
        Debug.Log("Missed");
      }
    }
  }
}
