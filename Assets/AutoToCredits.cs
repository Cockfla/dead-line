using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoToCredits : MonoBehaviour
{
    [SerializeField] private GameObject winObject; // ahora está en un objeto independiente
    public float delay = 3f;
    private bool creditsTriggered = false;

    void Update()
    {
        if (winObject.activeSelf && !creditsTriggered)
        {
            creditsTriggered = true;
            Debug.Log("WinScreen activo, iniciando cuenta regresiva...");
            StartCoroutine(WaitAndLoadCredits());
        }
    }

    private System.Collections.IEnumerator WaitAndLoadCredits()
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Cargando escena de créditos...");
        SceneManager.LoadScene("CreditsScene");
    }

    void OnDisable()
    {
        Debug.Log("⚠️ AutoToCredits desactivado");
    }
}
