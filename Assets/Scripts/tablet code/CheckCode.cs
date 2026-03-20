using System.Collections.Generic;
using UnityEngine;

namespace tablet_code
{
    public class CheckCode : MonoBehaviour
    {
        public MainEmail email;
        private List<Discrepancy> Discrepancies;
        private bool foundDiscrepancy = false;

        public void Check(GameObject clickedButton)
        {
            foreach (Discrepancy d in email.GetDiscrepancies())
            {
                if (d.GetDiscrepancyString() == email.GetHighlightedWord())
                {
                    foundDiscrepancy = true;
                    if (d.GetType() == clickedButton.tag)
                    {
                        print("correct button used");
                    }
                    else
                    {
                        print("wrong button used");
                    }

                }
            }

            if (!foundDiscrepancy)
            {
                print("not a discrepancy");
            }
            else
            {
                foundDiscrepancy = false;
            }
           
        }
    }
}
