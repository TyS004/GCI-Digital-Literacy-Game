using UnityEngine;

public class Menu_Script : MonoBehaviour
{
    public GameObject emailCamera;
    public GameObject phoneCamera;
    public GameObject MenuPanel;
    void Start()
    {
        MenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick()
    {
        MenuPanel.SetActive(!MenuPanel.activeSelf);
    }
    public void switchToEmail()
    {
        emailCamera.SetActive(true);
        phoneCamera.SetActive(false);
    }
    public void switchToPhone()
    {
        emailCamera.SetActive(false);
        phoneCamera.SetActive(true);
    }
}
