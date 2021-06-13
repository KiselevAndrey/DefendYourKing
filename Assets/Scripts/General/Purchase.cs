public class Purchase
{
    public readonly UnityEngine.Sprite icon;
    public readonly UnityEngine.Sprite interpretationBackground;
    public readonly string interpretation;
    public readonly int maxNumberOfPurchases;
    public readonly int numberInArray;

    private int _currentNumberOfPurchases;
    private int _basicCost;

    #region Constructor
    public Purchase(UnityEngine.Sprite icon, int maxNumberOfPurchases, UnityEngine.Sprite interpretationBackground, int numberInArray, string interpretation)
    {
        this.icon = icon;
        this.maxNumberOfPurchases = maxNumberOfPurchases;
        this.interpretationBackground = interpretationBackground;
        this.numberInArray = numberInArray;
        this.interpretation = interpretation;
        _currentNumberOfPurchases = 0;
    }

    public Purchase(PurchaseSO purchase, int numberInArray)
    {
        icon = purchase.icon;
        interpretation = purchase.interpretation;
        maxNumberOfPurchases = purchase.maxNumberOfPurchases;
        _basicCost = purchase.basicCost;

        this.numberInArray = numberInArray;

        _currentNumberOfPurchases = 0;
    }
    #endregion

    public void AddPurchases()
    {
        _currentNumberOfPurchases++;
    }

    public int CalculateCost()
    {
        return _basicCost * (_currentNumberOfPurchases + 1);
    }

    public bool CanBuyMore => _currentNumberOfPurchases < maxNumberOfPurchases;

    public void ChangeBasicCost(int value) => _basicCost = value;

    public void Reset()
    {
        _currentNumberOfPurchases = 0;
    }
}
