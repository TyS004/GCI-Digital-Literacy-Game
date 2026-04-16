using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RecapMainEmail : MonoBehaviour
{
    public TMP_Text EmailText;
    public Image SenderImage;

    private string OriginalText;
    private List<Discrepancy> Discrepancies = new List<Discrepancy>();

    public void ChangeMainEmail(Email email)
    {
        EmailText.text = email.GetFullText();
        OriginalText = EmailText.text;
        SenderImage.sprite = email.GetProfileImageSprite();
        Discrepancies = email.GetDiscrepancies();
        StartCoroutine(HighlightAfterUpdate());
    }

    private IEnumerator HighlightAfterUpdate()
    {
        yield return null;
        HighlightDiscrepancies();
    }

    void Awake()
    {
        if (EmailText == null) EmailText = GetComponent<TMP_Text>();
        OriginalText = EmailText.text;
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
}
