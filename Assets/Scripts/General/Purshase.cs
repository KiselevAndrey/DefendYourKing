using UnityEngine;

public class Purshase
{
    public readonly Sprite icon;
    public readonly int maxNumberOfPurchases;
    public readonly Sprite interpretation;

    public int CurrentNumberOfPurchases { get; private set; }

    public Purshase(Sprite icon, int maxNumberOfPurchases, Sprite interpretation)
    {
        this.icon = icon;
        this.maxNumberOfPurchases = maxNumberOfPurchases;
        this.interpretation = interpretation;
        CurrentNumberOfPurchases = 0;
    }

    public void AddPurchases()
    {
        CurrentNumberOfPurchases++;
    }
}
