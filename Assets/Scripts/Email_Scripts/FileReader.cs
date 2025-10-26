using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    public ProfileImageManager ProfileImageManager;

    // Path relative to the Unity project
    private string emailsPath;

    private void Awake()
    {
        // Application.dataPath points to the Assets folder
        emailsPath = Path.Combine(Application.dataPath, "Text/Emails/emails.txt");
    }

    public List<Email> Load()
    {
        if (!File.Exists(emailsPath))
        {
            Debug.LogError("Emails file not found: " + emailsPath);
            return new List<Email>();
        }

        return ReadEmailsAndImagesFromFile();
    }

    private List<Email> ReadEmailsAndImagesFromFile()
    {
        List<Email> emails = new List<Email>();
        string[] emailBlocks = File.ReadAllText(emailsPath)
            .Split(new string[] { "\r\n\r\n", "\n\n" }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string block in emailBlocks)
        {
            string from = "";
            string subject = "";
            string body = "";
            string[] lines = block.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (line.StartsWith("From: ")) from = line.Substring(6).Trim();
                else if (line.StartsWith("Subject: ")) subject = line.Substring(9).Trim();
                else if (line.StartsWith("Body: ")) body = line.Substring(6).Trim();
            }

            Sprite profileImageSprite = ProfileImageManager.GetProfile(from);
            emails.Add(new Email(from, subject, body, profileImageSprite));
        }

        return emails;
    }
}