public class BarrackBuyer : Buyer, IBuyer
{
    [UnityEngine.SerializeField] private Barrack barrack;

    public new void TryBuy(Purchase purshase)
    {
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
    }
}
