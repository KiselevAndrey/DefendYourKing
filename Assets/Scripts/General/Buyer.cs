using UnityEngine;

public class Buyer : MonoBehaviour, IBuyer
{
    [Header("Parameters")]
    [SerializeField] private int maxDifferentPurchases = 5;
    [SerializeField] private int maxAllPurchases;

    [Header("Purchases")]
    [SerializeField] private Sprite[] purchasesIcons;
    [SerializeField] private int[] purchasesMaximum;
    [SerializeField] private Sprite[] purchasesInterpretation;

    private Purshase[] _purshases;
    private int _currentCountPurchases;

    private void Start()
    {
        maxDifferentPurchases = purchasesIcons.Length;

        _purshases = new Purshase[maxDifferentPurchases];
        for (int i = 0; i < _purshases.Length; i++)
        {
            _purshases[i] = new Purshase(purchasesIcons[i], purchasesMaximum[i], purchasesInterpretation[i], i);
        }
    }

    #region Property
    public int MaxCountPurchases => maxAllPurchases;

    public Vector3 Position => transform.position;

    public Purshase[] Purshases => _purshases;
    #endregion

    public void TryBuy(Purshase purshase)
    {
        print(name);
        print("buy " + purshase);
    }
}
