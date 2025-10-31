using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MainEmail : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text EmailText;
    public Image SenderImage;
    private string originalText;
    private HashSet<int> highlightedWordIndices = new HashSet<int>();
    
    public void ChangeMainEmail(Email email)
    {
        EmailText.text = email.GetFullText();
        originalText = EmailText.text;
        highlightedWordIndices.Clear();
        SenderImage.sprite = email.GetProfileImageSprite();
    }

    void Awake()
    {
        if (EmailText == null) EmailText = GetComponent<TMP_Text>();
        originalText = EmailText.text;
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
        if (highlightedWordIndices.Contains(wordIndex))
        {
            highlightedWordIndices.Remove(wordIndex);
        }
        else
        {
            highlightedWordIndices.Add(wordIndex);
        }

        UpdateHighlightedText();
    }

    private void UpdateHighlightedText()
    {
        EmailText.text = originalText;
        EmailText.ForceMeshUpdate();

        string newText = originalText;

        int offset = 0;

        for (int i = 0; i < EmailText.textInfo.wordCount; i++)
        {
            if (!highlightedWordIndices.Contains(i)) continue;

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
        foreach (int i in highlightedWordIndices)
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
}
