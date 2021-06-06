using UnityEngine;

public class Purshase
{
    public readonly Sprite icon;
    public readonly int maxNumberOfPurchases;

    public int CurrentNumberOfPurchases { get; private set; }

    public Purshase(Sprite icon, int maxNumberOfPurchases)
    {
        this.icon = icon;
        this.maxNumberOfPurchases = maxNumberOfPurchases;
        CurrentNumberOfPurchases = 0;
    }

    public void AddPurchases()
    {
        CurrentNumberOfPurchases++;
    }
}
