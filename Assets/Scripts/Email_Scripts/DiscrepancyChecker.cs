using UnityEngine;
using System.Collections.Generic;

public class DiscrepancyChecker : MonoBehaviour
{
    [Header("Reference")]
    public MainEmail mainEmail;

    [Header("Keybinds")]
    public KeyCode spellingKey = KeyCode.P;
    public KeyCode grammarKey = KeyCode.G;
    public KeyCode phishingKey = KeyCode.L;

    void Update()
    {
        if (mainEmail == null) return;

        if (Input.GetKeyDown(spellingKey))
            CheckDiscrepancy("Spelling");

        if (Input.GetKeyDown(grammarKey))
            CheckDiscrepancy("Grammar");

        if (Input.GetKeyDown(phishingKey))
            CheckDiscrepancy("Phishing");
    }

    void CheckDiscrepancy(string typeToCheck)
    {
        string highlightedWord = mainEmail.GetHighlightedWord();

        if (string.IsNullOrEmpty(highlightedWord))
        {
            Debug.Log("No word highlighted.");
            return;
        }

        List<Discrepancy> discrepancies = mainEmail.GetDiscrepancies();

        foreach (Discrepancy d in discrepancies)
        {
            // Compare TYPE (string)
            if (d.GetDiscrepancyType() == typeToCheck)
            {
                // Compare WORD
                if (d.GetDiscrepancyString() == highlightedWord)
                {
                    Debug.Log($" CORRECT: '{highlightedWord}' is a {typeToCheck} discrepancy.");
                    return;
                }
            }
        }

        Debug.Log($" '{highlightedWord}' is NOT a {typeToCheck} discrepancy.");
    }
}