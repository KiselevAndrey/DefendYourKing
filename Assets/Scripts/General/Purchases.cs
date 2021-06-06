using UnityEngine;

public class Purchases : MonoBehaviour, IPurchases
{
    [Header("Parameters")]
    [SerializeField] private int maxDifferentPurchases = 5;
    [SerializeField] private int maxAllPurchases;
    [SerializeField] private Sprite[] purchasesIcons;

    private Purshase[] purshases;
    private int _currentCountPurchases;

    private void Start()
    {
        
    }

    #region Property
    public int MaxCountPurchases => maxAllPurchases;
    #endregion

    public void Buy()
    {
        
    }
}
