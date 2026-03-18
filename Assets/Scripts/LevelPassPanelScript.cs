using UnityEngine;
using UnityEngine.UI;

public class LevelPassPanelScript : MonoBehaviour
{
    public GameObject PassReslutScreen;
    public GameObject failReslutScreen;
    public Text PassText;
    public Text FailText;
    
    public LevelCompleteScript LevelCompleteScript;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowResults();
        }
    }

    public void ShowResults()
    {
        if (LevelCompleteScript.CheckIfPass())
        {
            PassReslutScreen.SetActive(true);
            PassText.text = "You passed and identified " + LevelCompleteScript.returncurrentScore() + " anomolies out of " + LevelCompleteScript.returnmaxScore();
        }
        else
        {
            failReslutScreen.SetActive(true);
            FailText.text = "You failed and identified " + LevelCompleteScript.returncurrentScore() +
                            " anomolies out of " + LevelCompleteScript.returnmaxScore();
        }
        
    }
}
