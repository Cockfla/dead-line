using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [Header("Botones")]
    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button mainMenuButton;
    public UnityEngine.UI.Button quitButton;
    public UnityEngine.UI.Button creditsButton;


    void Start()
    {
        // Configurar botones
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        if (creditsButton != null)
            creditsButton.onClick.AddListener(GoToCredits);

    }

    void RestartGame()
    {
        // Restaurar tiempo normal y ocultar mouse
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    void GoToMainMenu()
    {
        // Restaurar tiempo normal y mostrar mouse
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Cambia "MainMenu" por el nombre de tu escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }
    
    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void GoToCredits()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("CreditsScene"); // Asegúrate de que el nombre coincida exactamente
    }

}