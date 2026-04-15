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
    private double TimeElapsed;

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
        TimeElapsed = 0;
        
        RecapEmailManager.SetInbox();
        UpdateHeader();
        UpdateResults();
        UpdatePoints();
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
        ResultsText.text = $"Correct Amount: {TotalCorrect}\nIncorrect Amount: {TotalIncorrect}";
    }

    private void UpdatePoints()
    {
        double correctMultiplier = 5.0;
        double timeMultiplier = 5.0;
        double points = (TotalCorrect * correctMultiplier) + (TimeElapsed * timeMultiplier);
        
        PointsText.text = $"Total Correct: {TotalCorrect} x {correctMultiplier}\nTime Elapsed: {TimeElapsed} x {timeMultiplier}\n_________\n\nTotal Points: {points}";
    }

}
