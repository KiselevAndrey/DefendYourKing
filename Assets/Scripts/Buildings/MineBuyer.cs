public class MineBuyer : Buyer, IBuyer
{
    [UnityEngine.SerializeField] private RubyMine mine;

    public new bool TryBuy(Purchase purchase, out string negativeResut)
    {
        if (!base.TryBuy(purchase, out negativeResut)) return false;

        switch (purchase.numberInArray)
        {
            case 0:
                mine.AddMiner();
                break;

            case 1:
                mine.LessHitToIncome();
                break;

            case 2:
                mine.AddIncome();
                break;

            default:
                break;
        }

        return true;
    }
}
