using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuyerNew : Buyer, IBuyer
{
    [Header("BuildBuyer References")]
    [SerializeField] private GeneralBuilding generalBuilding;

    private new void Start()
    {
        maxAllPurchases = purchaseSOs.Length;
        base.Start();
    }

    public new bool TryBuy(Purchase purchase, out string negativeResut)
    {
        if (!base.TryBuy(purchase, out negativeResut)) return false;

        switch (purchase.numberInArray)
        {
            case 0:
                //generalBuilding.Build(0);
                break;

            default:
                break;
        }

        return true;
    }
}
