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
    protected int _currentCountPurchases;

    private void Start()
    {
        CreatePurchases();
    }

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

    public bool TryBuy(Purchase purshase, out string negativeResut)
    {
        negativeResut = "";
        print(name);
        print("buy " + purshase.interpretation);

        return true;
    }
}
