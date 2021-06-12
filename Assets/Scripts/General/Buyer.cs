using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour, IBuyer
{
    [Header("Parameters")]
    [SerializeField] private int maxDifferentPurchases = 5;
    [SerializeField] protected int maxAllPurchases;

    [Header("Purchases")]
    [SerializeField] private PurchaseSO[] purchaseSOs;

    private List<Purchase> _purchases;
    private IUnit unit;
    protected int _currentCountPurchases;

    #region Awake Start
    private void Awake()
    {
        unit = GetComponent<IUnit>();
    }

    private void Start()
    {
        CreatePurchases();
    }
    #endregion

    private void CreatePurchases()
    {
        maxDifferentPurchases = purchaseSOs.Length;

        _purchases = new List<Purchase>();
        for (int i = 0; i < maxDifferentPurchases; i++)
        {
            _purchases.Add(new Purchase(purchaseSOs[i], i));
        }
    }

    #region Property
    public int MaxCountPurchases => maxAllPurchases;

    public Vector3 Position => transform.position;

    public List<Purchase> Purchases => _purchases;
    #endregion

    #region TryBuy
    public bool TryBuy(Purchase purchase, out string negativeResut)
    {
        print(name + " try buy " + purchase.interpretation);

        negativeResut = "";
        int purchaseCost = purchase.CalculateCost();

        if (purchaseCost > unit.Player.Ruby)
        {
            negativeResut = "Need more rubies";
            return false;
        }

        if (_currentCountPurchases >= maxAllPurchases)
        {
            negativeResut = "No more buying here";
            return false;
        }

        if (!purchase.CanBuyMore)
        {
            negativeResut = "You can't buy it anymore";
            return false;
        }

        unit.Player.SpendRuby(purchaseCost);
        purchase.AddPurchases();

        return true;
    }
    #endregion
}
