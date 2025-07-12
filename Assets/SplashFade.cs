using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; // <--- ¡Esto es lo que faltaba!

public class SplashFade : MonoBehaviour
{
    public Image logoImage;            // Tu imagen del logo
    public float fadeDuration = 2f;    // Tiempo del fade in
    public float waitTime = 5f;        // Tiempo antes de ir al menú

    void Start()
    {
        StartCoroutine(FadeInAndLoad());
    }

    IEnumerator FadeInAndLoad()
    {
        // Inicial: invisible
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

        // Esperar
        yield return new WaitForSeconds(waitTime);

        // Cargar siguiente escena
        SceneManager.LoadScene("MainMenu");
    }
}
