using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Slider : MonoBehaviour
{
    public GameObject sliderPrefab;
    public EmailManager EmailManager;
    public Camera cam;

    public Vector3 SliderAnimationDistance;

    private bool isShown = false;
    private bool isMoving = false;
    private float StartPoint;
    private float EndPoint;

    private void Start()
    {
        StartPoint = cam.WorldToScreenPoint(new Vector3(0, 0, 90)).x;
        EndPoint = cam.WorldToScreenPoint(new Vector3(-36, 0, 90)).x;
        
    }

    public void Accept()
    {
        EmailManager.AcceptOrDeny(true);
    }
    
    public void Deny()
    {
        EmailManager.AcceptOrDeny(false);
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
            distance = EndPoint - StartPoint;
        }
        else
        {
            isShown = true;
            distance = (EndPoint - StartPoint) *-1;
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