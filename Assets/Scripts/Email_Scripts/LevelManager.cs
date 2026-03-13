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
        EmailManager.SetInbox(Levels[currentLevel].GetInbox());
        currentLevel++;
        
    }
}