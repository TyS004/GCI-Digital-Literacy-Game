using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public string emailScene;
    public string phoneScene;
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
        if (emailScene != null) SceneManager.LoadScene(emailScene);
    }
    public void switchToPhone()
    {
        if (emailScene != null) SceneManager.LoadScene(phoneScene);
    }
}
