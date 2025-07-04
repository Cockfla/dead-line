using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimit : MonoBehaviour
{
    public Transform target; // El jugador u objeto a seguir
    public SpriteRenderer background; // El fondo que limita la cámara

    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        // Calcula los límites del fondo
        float minX = background.bounds.min.x + halfWidth;
        float maxX = background.bounds.max.x - halfWidth;
        float minY = background.bounds.min.y + halfHeight;
        float maxY = background.bounds.max.y - halfHeight;

        // Si el fondo es más pequeño que la cámara, centra la cámara en el fondo
        if (background.bounds.size.x < halfWidth * 2)
        {
            minX = maxX = background.bounds.center.x;
        }
        if (background.bounds.size.y < halfHeight * 2)
        {
            minY = maxY = background.bounds.center.y;
        }

        // Limita la posición de la cámara
        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}