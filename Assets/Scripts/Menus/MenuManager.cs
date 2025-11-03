using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject guidePanel; 
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowGuide()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(true);
        }
    }
    public void HideGuide()
    {
        if (guidePanel != null)
        {
            guidePanel.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
