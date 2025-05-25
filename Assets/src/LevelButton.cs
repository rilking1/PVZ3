using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public UIManager uiManager;

    public void OnButtonClicked()
    {
        uiManager.StartLevel(levelIndex);
    }
}
