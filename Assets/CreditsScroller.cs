using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float stopYPosition = 600f; // Altura máxima a la que se detiene
    public float returnDelay = 5f;     // Segundos antes de volver al menú

    private RectTransform rectTransform;
    private bool hasStopped = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!hasStopped)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            if (rectTransform.anchoredPosition.y >= stopYPosition)
            {
                hasStopped = true;
                Invoke("ReturnToMenu", returnDelay);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMenu();
        }
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
