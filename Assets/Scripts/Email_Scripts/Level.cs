using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private List<Email> Inbox;
    private int LevelNumber;

    public Level(int levelNumber, List<Email> inbox)
    {
        LevelNumber = levelNumber;
        Inbox = inbox;
    }

    public List<Email> GetInbox()
    {
        return Inbox;
    }
    
}
