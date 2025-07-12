using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Asignados desde el Inspector
    public string gameSceneName = "GameScene";
    public string creditsSceneName = "CreditsScene";
    public GameObject optionsPanel;
    public GameObject buttonGroup;

    public void StartNewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        buttonGroup.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        buttonGroup.SetActive(true);
    }

}

