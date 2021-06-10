using UnityEngine;

public class Buyer : MonoBehaviour, IBuyer
{
    [Header("Parameters")]
    [SerializeField] private int maxDifferentPurchases = 5;
    [SerializeField] protected int maxAllPurchases;

    [Header("Purchases")]
    [SerializeField] private PurchaseSO[] purchaseSOs;
    //[SerializeField] private Sprite[] purchasesIcons;
    //[SerializeField] private int[] purchasesMaximum;
    //[SerializeField] private Sprite[] purchasesInterpretationBackground;
    //[SerializeField] private string[] purshasesInterpretations;

    private Purchase[] _purchases;
    protected int _currentCountPurchases;

    private void Start()
    {
        CreatePurchases();
    }

    private void CreatePurchases()
    {
        maxDifferentPurchases = purchaseSOs.Length;

        _purchases = new Purchase[maxDifferentPurchases];
        for (int i = 0; i < _purchases.Length; i++)
        {
            //_purchases[i] = new Purchase(purchasesIcons[i], purchasesMaximum[i], purchasesInterpretationBackground[i], i, purshasesInterpretations[i]);
            _purchases[i] = new Purchase(purchaseSOs[i], i);
        }
    }

    #region Property
    public int MaxCountPurchases => maxAllPurchases;

    public Vector3 Position => transform.position;

    public Purchase[] Purshases => _purchases;
    #endregion

    public bool TryBuy(Purchase purshase, ref string negativeResut)
    {
        print(name);
        print("buy " + purshase.interpretation);

        return true;
    }
}
