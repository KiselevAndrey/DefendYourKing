using UnityEngine;

[CreateAssetMenu(fileName = nameof(PurchaseSO))]
public class PurchaseSO : ScriptableObject
{
    public Sprite icon;
    public string interpretation;
    public int maxNumberOfPurchases;
    public int basicCost;
}
