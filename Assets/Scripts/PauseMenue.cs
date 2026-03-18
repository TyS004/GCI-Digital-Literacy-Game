using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenue : MonoBehaviour
{
    public GameObject pauseMenu;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
          PausAndUnpause();
        }
    }

    public void PausAndUnpause()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    
}
