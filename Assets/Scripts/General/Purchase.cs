using UnityEngine;

public class Purchase
{
    public readonly Sprite icon;
    public readonly Sprite interpretationBackground;
    public readonly string interpretation;
    public readonly int maxNumberOfPurchases;
    public readonly int numberInArray;

    public int CurrentNumberOfPurchases { get; private set; }

    public Purchase(Sprite icon, int maxNumberOfPurchases, Sprite interpretationBackground, int numberInArray, string interpretation)
    {
        this.icon = icon;
        this.maxNumberOfPurchases = maxNumberOfPurchases;
        this.interpretationBackground = interpretationBackground;
        this.numberInArray = numberInArray;
        this.interpretation = interpretation;
        CurrentNumberOfPurchases = 0;
    }

    public void AddPurchases()
    {
        CurrentNumberOfPurchases++;
    }
}
