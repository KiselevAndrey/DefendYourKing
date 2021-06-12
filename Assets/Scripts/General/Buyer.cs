using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour, IBuyer
{
    [Header("Parameters")]
    [SerializeField] protected int maxAllPurchases;

    [Header("Purchases")]
    [SerializeField] private PurchaseSO[] purchaseSOs;

    private List<Purchase> _purchases = new List<Purchase>();
    private IUnit unit;
    protected int _currentCountPurchases;
    private int _maxDifferentPurchases;

    #region Awake Start
    protected void Awake()
    {
        unit = GetComponent<IUnit>();
    }

    protected void Start()
    {
        CreatePurchases();
    }
    #endregion

    private void CreatePurchases()
    {
        _maxDifferentPurchases = purchaseSOs.Length;

        _purchases.Clear();
        for (int i = 0; i < _maxDifferentPurchases; i++)
        {
            _purchases.Add(new Purchase(purchaseSOs[i], i));
        }
    }

    #region Property
    public int MaxCountPurchases => maxAllPurchases;

    public Vector3 Position => transform.position;

    public List<Purchase> Purchases => _purchases;

    public bool IsActive { get; set; }
    #endregion

    #region TryBuy
    public bool TryBuy(Purchase purchase, out string negativeResut)
    {
        negativeResut = "";

        if (!IsActive)
        {
            negativeResut = "Buyer is not active";
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

        int purchaseCost = purchase.CalculateCost();
        if (purchaseCost > unit.Player.Ruby)
        {
            negativeResut = "Need more rubies";
            return false;
        }


        unit.Player.SpendRuby(purchaseCost);
        _currentCountPurchases++;
        purchase.AddPurchases();

        return true;
    }
    #endregion
}
