using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Slider : MonoBehaviour
{
    public GameObject sliderPrefab;
    public EmailManager EmailManager;

    public int SliderAnimationDistance;

    private bool isShown = false;
    private bool isMoving = false;

    public void Accept()
    {
        EmailManager.Accept();
        OnSliderClick();
    }
    
    public void Deny()
    {
        EmailManager.Deny();
        OnSliderClick();
    }

    public void OnSliderClick()
    {
        if (!isMoving)
            StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        isMoving = true;
        float distance;

        if (isShown)
        {
            isShown = false;
            distance = -SliderAnimationDistance;
        }
        else
        {
            isShown = true;
            distance = SliderAnimationDistance;
        }

        float elapsed = 0f;
        Vector3 startPos = sliderPrefab.transform.position;

        while (elapsed < SliderGameParameters.SliderAnimationDuration)
        {
            float t = elapsed / SliderGameParameters.SliderAnimationDuration;
            sliderPrefab.transform.position = startPos + Vector3.left * (t * distance);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sliderPrefab.transform.position = startPos + Vector3.left * distance;
        isMoving = false;
    }
}