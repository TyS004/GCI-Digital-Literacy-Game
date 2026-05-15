using UnityEngine;
using UnityEngine.UI;

public class RecapPanelManager : MonoBehaviour
{
    public Canvas RecapCanvas;
    public Text HeaderText;
    public Text ResultsText;
    
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
        UpdateRecapPanel();
    }

    private void UpdateRecapPanel()
    {
        TotalCorrect = LevelManager.GetTotalCorrect();
        TotalIncorrect = LevelManager.GetTotalIncorrect();
        
        RecapEmailManager.SetInbox();
        UpdateHeader();
        UpdateResults();
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
        HeaderText.text = $"Level {LevelManager.GetCurrentLevel()} Recap";
    }
    
    private void UpdateResults()
    {
        ResultsText.text = $"Correct Amount: {TotalCorrect}\t\tIncorrect Amount: {TotalIncorrect}";
    }

}
