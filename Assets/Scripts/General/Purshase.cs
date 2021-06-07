using UnityEngine;

public class Purshase
{
    public readonly Sprite icon;
    public readonly Sprite interpretation;
    public readonly int maxNumberOfPurchases;
    public readonly int numberInArray;

    public int CurrentNumberOfPurchases { get; private set; }

    public Purshase(Sprite icon, int maxNumberOfPurchases, Sprite interpretation, int numberInArray)
    {
        this.icon = icon;
        this.maxNumberOfPurchases = maxNumberOfPurchases;
        this.interpretation = interpretation;
        this.numberInArray = numberInArray;
        CurrentNumberOfPurchases = 0;
    }

    public void AddPurchases()
    {
        CurrentNumberOfPurchases++;
    }
}
