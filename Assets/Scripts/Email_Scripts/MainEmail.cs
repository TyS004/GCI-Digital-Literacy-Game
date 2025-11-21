using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class MainEmail : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text EmailText;
    public Image SenderImage;
    private string OriginalText;
    private HashSet<int> HighlightedWordIndices = new HashSet<int>();
    private string HighlightedWord; // make so only one word to be clicked
    private List<Discrepancy> Discrepancies = new List<Discrepancy>();
    
    public void ChangeMainEmail(Email email)
    {
        EmailText.text = email.GetFullText();
        OriginalText = EmailText.text;
        HighlightedWordIndices.Clear();
        SenderImage.sprite = email.GetProfileImageSprite();
        Discrepancies = email.GetDiscrepancies();
    }
    void Awake()
    {
        if (EmailText == null) EmailText = GetComponent<TMP_Text>();
        OriginalText = EmailText.text;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(EmailText, eventData.position, eventData.pressEventCamera);
        if (wordIndex != -1)
        {
            ToggleWord(wordIndex);
        }
    }
    private void ToggleWord(int wordIndex)
    {
        if (HighlightedWordIndices.Contains(wordIndex))
        {
            HighlightedWordIndices.Remove(wordIndex);
        }
        else
        {
            HighlightedWordIndices.Add(wordIndex);
        }
        UpdateHighlightedText();
    }
    private void UpdateHighlightedText()
    {
        EmailText.text = OriginalText;
        EmailText.ForceMeshUpdate();
        string newText = OriginalText;
        int offset = 0;
        for (int i = 0; i < EmailText.textInfo.wordCount; i++)
        {
            if (!HighlightedWordIndices.Contains(i)) continue;
            TMP_WordInfo wordInfo = EmailText.textInfo.wordInfo[i];
            int startIndex = wordInfo.firstCharacterIndex + offset;
            int length = wordInfo.characterCount;
            
            string colorStart = $"<color={EmailParameters.HighlightedWordColor}>";
            string colorEnd = "</color>";
            // insert color tags into the correct positions
            newText = newText.Insert(startIndex, colorStart);
            offset += colorStart.Length;
            newText = newText.Insert(startIndex + length + colorStart.Length, colorEnd);
            offset += colorEnd.Length;
        }
        EmailText.text = newText;
        EmailText.ForceMeshUpdate();
    }
    public List<string> GetHighlightedWords()
    {
        List<string> words = new List<string>();
        foreach (int i in HighlightedWordIndices)
        {
            TMP_WordInfo wordInfo = EmailText.textInfo.wordInfo[i];
            words.Add(wordInfo.GetWord());
        }
        return words;
    }
    
    public void PrintHighlightedWords()
    {
        List<string> words = GetHighlightedWords();
        string wordstring = "";
        foreach (string word in words)
            wordstring += word + ",";
        print(wordstring);
    }
    public void HighlightDiscrepancies()
    { 
        PrintDiscrepancies();
        
        if (Discrepancies == null || Discrepancies.Count == 0)
        {
            return;
        }
        string newText = OriginalText;
        EmailText.ForceMeshUpdate();
        int offset = 0;
        for (int i = 0; i < EmailText.textInfo.wordCount; i++)
        {
            TMP_WordInfo wordInfo = EmailText.textInfo.wordInfo[i];
            string word = wordInfo.GetWord();
            foreach (Discrepancy d in Discrepancies)
            {
                if (word == d.GetDiscrepancyString())
                {
                    string colorStart = $"<color={EmailParameters.DiscrepancyColor}>";
                    string colorEnd = "</color>";
                    int startIndex = wordInfo.firstCharacterIndex + offset;
                    int length = wordInfo.characterCount;
                    newText = newText.Insert(startIndex, colorStart);
                    offset += colorStart.Length;
                    newText = newText.Insert(startIndex + length + colorStart.Length, colorEnd);
                    offset += colorEnd.Length;
                    break;
                }
            }
        }
        EmailText.text = newText;
        EmailText.ForceMeshUpdate();
    }

    public List<Discrepancy> GetDiscrepancies()
    {
        return Discrepancies;
    }
    
    private void PrintDiscrepancies()
    {
        if (Discrepancies == null || Discrepancies.Count == 0)
        {
            print("No discrepancies found.");
            return;
        }
        foreach (Discrepancy d in Discrepancies)
        {
            print($"Type: {d.GetType()}, Text: {d.GetDiscrepancyString()}");
        }
    }
    
    public string GetHighlightedWord()
    {
        return HighlightedWord;
    }
}
