using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Canvas PauseMenuCanvas;
    public LevelManager LevelManager;
    public RecapPanelManager RecapPanelManager;
    public MainMenuScript MainMenuScript;
    public TabletManager TabletManager;

    void Start()
    {
        Hide();
    }

    public void Show()
    {
        PauseMenuCanvas.enabled = true;
    }

    public void Hide()
    {
        PauseMenuCanvas.enabled = false;
    }

    public void Reset()
    {
        Hide();
        RecapPanelManager.Hide();
        TabletManager.CloseTablet();
        MainMenuScript.Show();
        LevelManager.Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }
}