using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Slider : MonoBehaviour
{
    public GameObject sliderPrefab;
    public Emails Emails;
    
    private bool isShown = false;
    private bool isMoving = false;
    
    void Start()
    {
        
    }

    public void OnAcceptClick()
    {
        Emails.Accept();
    }
    
    public void OnDenyClick()
    {
        Emails.Deny();
    }

    public void OnSliderClick()
    {
        if (!isMoving)
            StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        isMoving = true;
        isShown = !isShown;
        
        SliderGameParameters.SliderAnimationDistance *= -1;

        float elapsed = 0f;
        Vector3 startPos = sliderPrefab.transform.position;

        while (elapsed < SliderGameParameters.SliderAnimationDuration)
        {
            float t = elapsed / SliderGameParameters.SliderAnimationDuration;
            sliderPrefab.transform.position = startPos + Vector3.right * (t * SliderGameParameters.SliderAnimationDistance);

            elapsed += Time.deltaTime;
            yield return null;
        }

        sliderPrefab.transform.position = startPos + Vector3.right * SliderGameParameters.SliderAnimationDistance;

        isMoving = false;
    }
}


