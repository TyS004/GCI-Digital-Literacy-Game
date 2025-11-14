using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class FileReader : MonoBehaviour
{
    public ProfileImageManager ProfileImageManager;
    private string emailsPath;
    private void Awake()
    {
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
            // Assign discrepancies and clean text
            List<Discrepancy> fromDiscrepancies = AssignDiscrepancies(ref from);
            List<Discrepancy> subjectDiscrepancies = AssignDiscrepancies(ref subject);
            List<Discrepancy> bodyDiscrepancies = AssignDiscrepancies(ref body);
            // Combine all discrepancies
            List<Discrepancy> allDiscrepancies = new List<Discrepancy>();
            allDiscrepancies.AddRange(fromDiscrepancies);
            allDiscrepancies.AddRange(subjectDiscrepancies);
            allDiscrepancies.AddRange(bodyDiscrepancies);
            Sprite profileImageSprite = ProfileImageManager.GetProfile(from);
            emails.Add(new Email(from, subject, body, profileImageSprite, allDiscrepancies));
        }
        return emails;
    }
    private List<Discrepancy> AssignDiscrepancies(ref string text)
    {
        List<Discrepancy> discrepancies = new List<Discrepancy>();
        if (string.IsNullOrEmpty(text)) return discrepancies;
        int searchIndex = 0;
        int charsRemoved = 0;
        while (searchIndex < text.Length)
        {
            int startTag = text.IndexOf("<d type=\"", searchIndex);
            if (startTag == -1) break;
            int typeStart = startTag + 9;
            int typeEnd = text.IndexOf("\"", typeStart);
            if (typeEnd == -1) break;
            string type = text.Substring(typeStart, typeEnd - typeStart);
            int contentStart = text.IndexOf(">", typeEnd);
            if (contentStart == -1) break;
            contentStart += 1;
            int contentEnd = text.IndexOf("</d>", contentStart);
            if (contentEnd == -1) break;
            string content = text.Substring(contentStart, contentEnd - contentStart);
            int openingTagLength = contentStart - startTag;
            int closingTagLength = 4;
            text = text.Substring(0, startTag) + content + text.Substring(contentEnd + closingTagLength);
            int wordOffset = 0;
            string[] words = content.Split(' ');
            foreach (string word in words)
            {
                int startIndex = startTag - charsRemoved + wordOffset;
                int endIndex = startIndex + word.Length - 1;
                discrepancies.Add(new Discrepancy(type, word, startIndex, endIndex));
                wordOffset += word.Length + 1;
            }
            charsRemoved += openingTagLength + closingTagLength;
            searchIndex = startTag + content.Length;
        }
        return discrepancies;
    }
}
