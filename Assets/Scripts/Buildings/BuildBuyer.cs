public class BuildBuyer : Buyer, IBuyer
{
    [UnityEngine.SerializeField] private IBuilding building;
    [UnityEngine.SerializeField] private int buildCost = -1;

    #region Awake Start
    private new void Awake()
    {
        base.Awake();

        building = GetComponent<IBuilding>();
    }
     private new void Start()
     {
        base.Start();
        
        if(buildCost > -1)
            Purchases[0].ChangeBasicCost(buildCost);
     }
    #endregion

    public new bool TryBuy(Purchase purchase, out string negativeResut)
    {
        if (!base.TryBuy(purchase, out negativeResut)) return false;

        switch (purchase.numberInArray)
        {
            case 0:
                building.Build();
                break;

            default:
                break;
        }

        return true;
    }
}
