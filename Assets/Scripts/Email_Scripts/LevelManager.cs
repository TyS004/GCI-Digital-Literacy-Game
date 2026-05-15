using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public FileReader FileReader;
    public EmailManager EmailManager;
    private List<Level> Levels;
    
    private int currentLevel;
    private int totalCorrect;
    private int totalIncorrect;

    public void Start()
    {
        Levels = FileReader.Load();
        currentLevel = 0;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevel == Levels.Count - 1)
        {
            
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