using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public FileReader FileReader;
    public EmailManager EmailManager;
    public Canvas GameOverCanvas;
    public Text GameOverStatsText;
    
    private List<Level> Levels;
    private int currentLevel;
    private int totalCorrect;
    private int totalIncorrect;

    public void Start()
    {
        Levels = FileReader.Load();
        Reset();
    }
    
    public void Reset()
    {
        totalCorrect = 0;
        totalIncorrect = 0;
        currentLevel = 0;
        LoadNextLevel();
        GameOverCanvas.enabled = false;
    }

    public void LoadNextLevel()
    {
        if (currentLevel == Levels.Count)
        {
            GameOverCanvas.enabled = true;
            GameOverStatsText.text = $"Total Correct: {totalCorrect}\nTotal Incorrect: {totalIncorrect}";
            return;
        }
        EmailManager.SetInbox(Levels[currentLevel].GetInbox());
        currentLevel++;
    }

    public void IncreaseTotalCorrect(int correctAmount)
    {
        totalCorrect += correctAmount;
    }
    
    public void IncreaseTotalIncorrect(int incorrectAmount)
    {
        totalIncorrect += incorrectAmount;
    }
    
    private bool CanContinue()
    {
        // can continue if incorrect amount is within a certain threshold
        return false;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetTotalCorrect()
    {
        return totalCorrect;
    }
    
    public int GetTotalIncorrect()
    {
        return totalIncorrect;
    }
}