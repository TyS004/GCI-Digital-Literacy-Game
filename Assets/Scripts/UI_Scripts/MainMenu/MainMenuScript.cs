using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public Canvas mainMenuPanel;

    public void OnStartClick()
    {
        mainMenuPanel.enabled = false;
    }
}
