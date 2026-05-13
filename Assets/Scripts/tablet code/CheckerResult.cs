using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckerResult : MonoBehaviour
{
    public Image ResultImage;
    public Text ResultText;

    public void Reset()
    {
        ResultImage.color = TabletParameters.DefaultColor;
        ResultText.text = "---";
    }
    
    public void Detected()
    {
        StopAllCoroutines();
        ResultImage.color = TabletParameters.DetectedColor;
        ResultText.text = "Discrepancy Detected";
        StartCoroutine(ResetAfterDelay());
    }

    public void NotDetected()
    {
        StopAllCoroutines();
        ResultImage.color = TabletParameters.NotDetectedColor;
        ResultText.text = "Discrepancy Not Detected";
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(TabletParameters.DisplayDuration);
        Reset();
    }
}