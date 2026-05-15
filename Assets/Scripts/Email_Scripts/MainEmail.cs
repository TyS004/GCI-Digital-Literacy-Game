using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class MainEmail : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text EmailText;
    public Image SenderImage;
    public GameObject ProfileOutline;
    public CheckerResult CheckerResult;
    
    private string OriginalText;
    private int HighlightedWordIndex = -1;
    private string HighlightedWord;
    private List<Discrepancy> Discrepancies = new List<Discrepancy>();
    private Button ProfileImageButton;
    private bool profileSelected = false;
    
    void Awake()
    {
        if (EmailText == null) EmailText = GetComponent<TMP_Text>();
        OriginalText = EmailText.text;
    
        ProfileImageButton = SenderImage.GetComponent<Button>();
        ProfileImageButton.onClick.AddListener(OnProfileImageClicked);
        ProfileOutline.SetActive(false);
    }
    
    public void ChangeMainEmail(Email email)
    {
        EmailText.text = email.GetFullText();
        OriginalText = EmailText.text;
        HighlightedWordIndex = -1;
        HighlightedWord = "";
        profileSelected = false;
        SenderImage.color = Color.white;
        ProfileOutline.SetActive(false);
        SenderImage.sprite = email.GetProfileImageSprite();
        Discrepancies = email.GetDiscrepancies();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(EmailText, eventData.position, eventData.pressEventCamera);
        if (wordIndex != -1)
        {
            profileSelected = false;
            SenderImage.color = Color.white;
            ProfileOutline.SetActive(false);
            ToggleWord(wordIndex);
        }
    }
    
    public List<Discrepancy> GetDiscrepancies()
    {
        return Discrepancies;
    }

    public void CheckDiscrepancy(string type)
    {
        foreach (Discrepancy d in Discrepancies)
        {
            if (profileSelected && d.GetDiscrepancyType() == "profile" && type == "profile")
            {
                CheckerResult.Detected();
                return;
            }
            if (d.GetDiscrepancyString() == HighlightedWord && type == d.GetDiscrepancyType())
            {
                CheckerResult.Detected();
                return;
            }
        }
        CheckerResult.NotDetected();
    }

    private void OnProfileImageClicked()
    {
        profileSelected = !profileSelected;
        //SenderImage.color = profileSelected ? Color.cornflowerBlue : Color.white;
        ProfileOutline.SetActive(profileSelected);

        HighlightedWordIndex = -1;
        HighlightedWord = "";
        UpdateHighlightedText();
    }
    
    private void ToggleWord(int wordIndex)
    {
        if (HighlightedWordIndex == wordIndex)
        {
            HighlightedWordIndex = -1;
            HighlightedWord = "";
        }
        else
        {
            HighlightedWordIndex = wordIndex;
            HighlightedWord = EmailText.textInfo.wordInfo[wordIndex].GetWord();
        }

        UpdateHighlightedText();
    }
    
    private void UpdateHighlightedText()
    {
        EmailText.text = OriginalText;
        EmailText.ForceMeshUpdate();

        if (HighlightedWordIndex == -1) return;

        string newText = OriginalText;
        int offset = 0;

        TMP_WordInfo wordInfo = EmailText.textInfo.wordInfo[HighlightedWordIndex];

        int startIndex = wordInfo.firstCharacterIndex + offset;
        int length = wordInfo.characterCount;

        string colorStart = $"<color={EmailParameters.HighlightedWordColor}>";
        string colorEnd = "</color>";
        
        newText = newText.Insert(startIndex, colorStart);
        offset += colorStart.Length;

        newText = newText.Insert(startIndex + length + colorStart.Length, colorEnd);
        //offset += colorEnd.Length;

        EmailText.text = newText;
        EmailText.ForceMeshUpdate();
    }
    
    
    //----------------------Methods for testing----------------------
    
    public string GetHighlightedWord()
    {
        if (HighlightedWordIndex != -1)
        {
            TMP_WordInfo wordInfo = EmailText.textInfo.wordInfo[HighlightedWordIndex];
            return wordInfo.GetWord();
        }
        return "";
    }
    
    public void PrintHighlightedWord()
    {
        string word = GetHighlightedWord();
        if (!string.IsNullOrEmpty(word))
            print(word);
        else
            print("No word highlighted");
    }
    
    public void HighlightDiscrepancies()
    {
        if (Discrepancies == null || Discrepancies.Count == 0) return;

        string newText = OriginalText;
        EmailText.text = OriginalText;
        EmailText.ForceMeshUpdate();
        int offset = 0;

        for (int i = 0; i < EmailText.textInfo.wordCount; i++)
        {
            TMP_WordInfo wordInfo = EmailText.textInfo.wordInfo[i];

            foreach (Discrepancy d in Discrepancies)
            {
                if (wordInfo.firstCharacterIndex == d.GetStartIndex())
                {
                    string colorStart = $"<color={EmailParameters.DiscrepancyColor}>";
                    string colorEnd = "</color>";
                    int startIndex = wordInfo.firstCharacterIndex + offset;
                    newText = newText.Insert(startIndex, colorStart);
                    offset += colorStart.Length;
                    newText = newText.Insert(startIndex + wordInfo.characterCount + colorStart.Length, colorEnd);
                    offset += colorEnd.Length;
                    break;
                }
            }
        }

        EmailText.text = newText;
        EmailText.ForceMeshUpdate();
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

    private bool HasDiscrepancy()
    {
        return Discrepancies.Count > 0;
    }
}
