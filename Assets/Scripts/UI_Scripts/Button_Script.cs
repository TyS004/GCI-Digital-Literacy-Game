using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject Button;
    public bool isAcceptButton;
    
    public StatsManager statsManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        if(isAcceptButton) statsManager.ProcessEmailDecision(false);
        else statsManager.ProcessEmailDecision(true);
    }
}
