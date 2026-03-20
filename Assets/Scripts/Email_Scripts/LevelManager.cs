using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public FileReader FileReader;
    public EmailManager EmailManager;
    private List<Level> Levels;
    private int currentLevel;

    public void Start()
    {
        Levels = FileReader.Load();
        currentLevel = 0;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (currentLevel != 0 && EmailManager.GetIncorrectAmount() > 0)
        {
            // end of level flash screen if correct or wrong
        }
        EmailManager.SetInbox(Levels[currentLevel].GetInbox());
        currentLevel++;
    }
    
    private bool CanContinue()
    {
        return false;
    }
}