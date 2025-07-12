using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // <-- Necesario para usar Image

public class IntroFade : MonoBehaviour
{
    public Image logoImage;            // Imagen del logo
    public float fadeDuration = 2f;    // Duración del fade-in
    public float waitTime = 5f;        // Tiempo de espera antes de cargar la escena

    void Start()
    {
        StartCoroutine(FadeInAndLoad());
    }

    IEnumerator FadeInAndLoad()
    {
        // Inicial: transparente
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

        // Espera después del fade-in
        yield return new WaitForSeconds(waitTime);

        // Cargar la siguiente escena
        SceneManager.LoadScene("GAME");
    }
}
