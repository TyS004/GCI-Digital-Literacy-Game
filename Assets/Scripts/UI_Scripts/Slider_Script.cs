using UnityEngine;
using UnityEngine.Rendering;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject sliderPrefab;

    public float sliderAnimationVelocity;
    
    public float sliderAnimationStopCoordinate;
    private float sliderAnimationStartCoordinate;
    
    private bool isOpen = false;
    private bool isMoving = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderAnimationStartCoordinate = sliderPrefab.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        print(isMoving + ", " + isOpen);
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

    public void OnClick()
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
    }
}


