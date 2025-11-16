using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject sliderPrefab;

    public float sliderAnimationVelocity;
    
    public float sliderAnimationStopCoordinate;
    private float sliderAnimationStartCoordinate;
    
    private bool isShown = false;
    private bool isMoving = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // testing testing branch
    // does this work?
    // wills branch push test
    // testsogjsidfgnsdlkjgbslkdjgb
    void Start()
    {
        //sliderAnimationStartCoordinate = sliderPrefab.transform.position.x;
    }

    /* Update is called once per frame
    public void OnSliderClickOld()
    {
        if (isMoving)
        {
            if (isOpen && sliderPrefab.transform.position.x > sliderAnimationStopCoordinate)
            {
                sliderPrefab.transform.position += Vector3.left * sliderAnimationVelocity * Time.deltaTime;
            }
            else if(!isOpen && sliderPrefab.transform.position.x < sliderAnimationStartCoordinate)
            {
                sliderPrefab.transform.position += Vector3.right * sliderAnimationVelocity * Time.deltaTime;
            }
        }
    }
*/

    public void OnSliderClick()
    {
        if (isMoving == false)
            StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        isMoving = true;
        float distance;

        if (isShown)
        { 
            isShown = false;
            distance = -50f;
        }
        else
        {
            isShown = true;
            distance = 10f;
        }
        
        float duration = 0.4f;
        float elapsed = 0f;
        Vector3 startPos = sliderPrefab.transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            sliderPrefab.transform.position = startPos + Vector3.left * (t * distance);
            elapsed += Time.deltaTime;
            yield return null;
        }

        sliderPrefab.transform.position = startPos + Vector3.left * distance;
        isMoving = false;
    }

   /* public void OnClick()
    {
        print("Button Clicked" + sliderPrefab.transform.position);

        isMoving = true;

        if (!isOpen)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }*/
}


