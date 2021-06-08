using UnityEngine;

public class Buyer : MonoBehaviour, IBuyer
{
    [Header("Parameters")]
    [SerializeField] private int maxDifferentPurchases = 5;
    [SerializeField] private int maxAllPurchases;

    [Header("Purchases")]
    [SerializeField] private Sprite[] purchasesIcons;
    [SerializeField] private int[] purchasesMaximum;
    [SerializeField] private Sprite[] purchasesInterpretationBackground;
    [SerializeField] private string[] purshasesInterpretations;

    private Purchase[] _purchases;
    private int _currentCountPurchases;

    private void Start()
    {
        CreatePurchases();
    }

    private void CreatePurchases()
    {
        maxDifferentPurchases = purchasesIcons.Length;

        _purchases = new Purchase[maxDifferentPurchases];
        for (int i = 0; i < _purchases.Length; i++)
        {
            _purchases[i] = new Purchase(purchasesIcons[i], purchasesMaximum[i], purchasesInterpretationBackground[i], i, purshasesInterpretations[i]);
        }
    }

    #region Property
    public int MaxCountPurchases => maxAllPurchases;

    public Vector3 Position => transform.position;

    public Purchase[] Purshases => _purchases;
    #endregion

    public void TryBuy(Purchase purshase)
    {
        print(name);
        print("buy " + purshase.interpretation);
    }
}
