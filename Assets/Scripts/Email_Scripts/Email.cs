using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Email
{
    private string From;
    private string Subject;
    private string Body;
    private Sprite ProfileImageSprite;
    private List<Discrepancy> Discrepancies;

    public Email(string from, string subject, string body, Sprite profileImageSprite, List<Discrepancy> discrepancies)
    {
        From = from;
        Subject = subject;
        Body = body;
        ProfileImageSprite = profileImageSprite;
        Discrepancies = discrepancies ?? new List<Discrepancy>();
    }

    public string GetFullText()
    {
        return $"From: {From}\nSubject: {Subject}\nBody: {Body}";
    }
    
    public Sprite GetProfileImageSprite()
    {
        return ProfileImageSprite;
    }
    
    public List<Discrepancy> GetDiscrepancies()
    {
        return Discrepancies;
    }
}
