using UnityEngine;
using UnityEngine.UI;

public class RecapPanelManager : MonoBehaviour
{
    public Canvas RecapCanvas;
    public Text HeaderText;
    public Text ResultsText;
    public Text PointsText;
    
    public RecapEmailManager RecapEmailManager;
    public LevelManager LevelManager;
    
    private int TotalCorrect;
    private int TotalIncorrect;

    public void Start()
    {
        Hide();
    }
    public void Show()
    {
        RecapCanvas.enabled = true;
        RecapEmailManager.SetInbox();
        UpdateStats();
    }

    public void Hide()
    {
        RecapCanvas.enabled = false;
    }

    public void OnNextLevelClicked()
    {
        Hide();
        LevelManager.LoadNextLevel();
    }

    private void UpdateHeader()
    {
        
    }
    
    private void UpdateStats()
    {
        TotalCorrect = LevelManager.GetTotalCorrect();
        TotalIncorrect = LevelManager.GetTotalIncorrect();
        
        
    }

}
