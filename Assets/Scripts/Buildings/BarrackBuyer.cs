public class BarrackBuyer : Buyer, IBuyer
{
    [UnityEngine.SerializeField] private Barrack barrack;

    public new bool TryBuy(Purchase purchase, out string negativeResut)
    {
        if (!base.TryBuy(purchase, out negativeResut)) return false;
        
        switch (purchase.numberInArray)
        {
            case 0:
                barrack.BuyUnits(0);
                break;

            case 1:
                barrack.BuyUnits(0, 2);
                break;

            default:
                break;
        }

        return true;
    }
}
