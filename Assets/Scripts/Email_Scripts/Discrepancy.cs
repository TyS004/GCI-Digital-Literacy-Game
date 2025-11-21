using UnityEngine;

public class Discrepancy
{
    private string Type;
    private string DiscrepancyString;
    private int StartIndex;
    private int EndIndex;
    
    public Discrepancy(string type, string discrepancyString, int startIndex = -1, int endIndex = -1)
    {
        Type = type;
        DiscrepancyString = discrepancyString;
        StartIndex = startIndex;
        EndIndex = endIndex;
    }
    
    new public string GetType()
    {
        return Type;
    }
    
    public string GetDiscrepancyString()
    {
        return DiscrepancyString;
    }
    
    public int GetStartIndex()
    {
        return StartIndex;
    }
    
    public int GetEndIndex()
    {
        return EndIndex;
    }
}
