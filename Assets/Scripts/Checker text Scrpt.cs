using UnityEngine;

public class CheckertextScrpt : MonoBehaviour
{
    public MainEmail MainEmail;
    
    
   public bool CheckGrammerOnClick()
    {
        foreach ( Discrepancy Discrepancy in MainEmail.GetDiscrepancies())
       {
            if (Discrepancy.GetDiscrepancyString() == MainEmail.GetHighlightedWords()[0])
            {
                return true;
            }
       }
       return false;
   }

   public void ShowResultGrammer()
   {
       if (CheckGrammerOnClick() == true)
       {
           print(" descrepancy detected");
       }

       else
       {
           print("discrepancy not detected");
       }
   }
}

