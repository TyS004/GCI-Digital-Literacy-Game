using UnityEngine;

[System.Serializable]
public class Email
{
    private string From;
    private string Subject;
    private string Body;
    private Sprite ProfileImageSprite;

    public Email(string from, string subject, string body, Sprite profileImageSprite)
    {
        From = from;
        Subject = subject;
        Body = body;
        ProfileImageSprite =  profileImageSprite;
    }

    public string GetFullText()
    {
        return $"From: {From}\nSubject: {Subject}\nBody: {Body}";
    }

    public Sprite GetProfileImageSprite()
    {
        return ProfileImageSprite;
    }
}