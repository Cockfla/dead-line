using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoToCredits : MonoBehaviour
{
    public GameObject triggerImageObject; // El GameObject que activará el inicio del fade
    public Image logoImage;               // Imagen a hacer fade
    public float fadeDuration = 2f;       // Duración del fade
    public float waitTime = 3f;           // Tiempo antes de cambiar de escena
    public string nextSceneName = "GAME"; // Nombre de la escena a cargar

    private bool started = false;

    void Update()
    {
        // Esperar a que se active el objeto (una sola vez)
        if (!started && triggerImageObject.activeSelf)
        {
            started = true;
            StartCoroutine(FadeInAndLoad());
        }
    }

    IEnumerator FadeInAndLoad()
    {
        // Iniciar con opacidad cero
        Color color = logoImage.color;
        color.a = 0f;
        logoImage.color = color;

        float timer = 0f;

        // Fade in
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            logoImage.color = color;
            yield return null;
        }

        // Espera adicional antes de cargar
        yield return new WaitForSeconds(waitTime);

        // Cambiar de escena
        SceneManager.LoadScene(nextSceneName);
    }
}
