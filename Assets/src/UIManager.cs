using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gameUIPanel;
    public GameObject winPanel;

    public LevelManager levelManager;
    public GameObject losePanel;


    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        gameUIPanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        mainMenuPanel.SetActive(false);
        gameUIPanel.SetActive(false);
        winPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        gameUIPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }


    public void StartLevel(int levelIndex)
    {
        mainMenuPanel.SetActive(false);
        gameUIPanel.SetActive(true);

        levelManager.StartLevel(levelIndex);
    }
    public void ShowLoseScreen()
    {
        mainMenuPanel.SetActive(false);
        gameUIPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }



}
