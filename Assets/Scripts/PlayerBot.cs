using System.Collections.Generic;
using UnityEngine;
using KAP;

public class PlayerBot : Player, IPlayer
{
    [Header("Bot Parameters")]
    [SerializeField, Min(0)] private float rubyModultiplier;

    private readonly List<IBuyer> _buyers = new List<IBuyer>();

    #region Awake Start
    private void Awake()
    {
        foreach(Transform child in transform.GetComponentInChildren<Transform>())
        {
            if (child.TryGetComponent(out IUnit unit)) unit.Player = this;
            if (child.TryGetComponent(out IBuyer buyer)) _buyers.Add(buyer);
            if (child.TryGetComponent(out BuildBuyer buildBuyer)) _buyers.Add(buildBuyer);
        }
    }

    private void Start()
    {
        Ruby = startedRubyCount;    
    }
    #endregion

    #region Select Units
    public new void SelectUnit(ISelectableUnit selectableUnit)
    {
        if (_selectedUnit == selectableUnit) return;

        _selectedUnit = selectableUnit;
    }

    public new void DeselectUnit(ISelectableUnit deselectedUnit)
    {
        if (deselectedUnit != _selectedUnit)
        {
            OnCheckDeselectUnit(deselectedUnit);
            return;
        }

        _selectedUnit = null;
    }
    #endregion

    #region Ruby
    public new int Ruby
    {
        get => _ruby;
        private set
        {
            _ruby = value;
        }
    }

    public new void AddRuby(int value)
    {
        Ruby += (int)(value * rubyModultiplier);

        TryBuyRandomPurchase();
    }

    private void TryBuyRandomPurchase()
    {
        IBuyer buyer = _buyers.Random();

        if (buyer.Purchases.Random(out Purchase purchase))
            if (!buyer.TryBuy(purchase, out string negativeResult))
                print(negativeResult);
    }

    public new void SpendRuby(int value)
    {
        Ruby -= value;
    }
    #endregion
}
