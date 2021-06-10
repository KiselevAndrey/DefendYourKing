public class BarrackBuyer : Buyer, IBuyer
{
    [UnityEngine.SerializeField] private Barrack barrack;

    public new bool TryBuy(Purchase purshase, ref string negativeResut)
    {
        if (_currentCountPurchases >= maxAllPurchases)
        {
            negativeResut = "Too many purchases";
            return false;
        }

        switch (purshase.numberInArray)
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
