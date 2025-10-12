using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class EmailReader
{
    private static string emailsFolderName = "Assets/Text/Emails";
    private static string emailsFileName = "emails.txt";
    private static string emailsPath
    {
        get { return Path.Combine(emailsFolderName, emailsFileName); }
    }
    
    public static List<string> Load(string defaultEmail)
    {
        EnsureFileExists(defaultEmail);
        return ReadEmailsFromFile();
    }

    private static void EnsureFileExists(string defaultEmail)
    {
        if (!File.Exists(emailsPath))
            CreateFile(defaultEmail);
    }

    private static void CreateFile(string defaultEmail)
    {
        EnsureDirectoryExists();
        
        StreamWriter writer = new StreamWriter(emailsPath);
        writer.Write(defaultEmail);
        writer.Close();
    }

    private static void EnsureDirectoryExists()
    {
        if (!Directory.Exists(emailsFolderName))
            Directory.CreateDirectory(emailsFolderName);
    }

    private static List<string> ReadEmailsFromFile()
    {
        List<string> emails = new List<string>();
    
        string[] lines = File.ReadAllText(emailsPath).Split("\n\n", System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string email in lines)
            emails.Add(email.Trim());

        return emails;
    }
}
