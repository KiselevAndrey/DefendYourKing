using System;
using System.Collections.Generic;
using UnityEngine;
using KAP;

public class PlayerBot : MonoBehaviour, IPlayer
{
    [Header("Parameters")]
    [SerializeField] private Material material;
    [SerializeField, Min(0)] private int startedRubyCount;
    [SerializeField, Min(0)] private float rubyModultiplier;

    [Header("References")]
    [SerializeField] private PathPoint startPathPoint;

    public ISeller seller;

    public static Action<ISelectableUnit> OnCheckDeselectUnit;

    private ISelectableUnit _selectedUnit;
    private int _ruby;
    private List<IBuyer> _buyers = new List<IBuyer>();

    #region Awake Start OnEnable OnDisable
    private void Awake()
    {
        foreach(Transform child in transform.GetComponentInChildren<Transform>())
        {
            if (child.TryGetComponent(out IUnit unit)) unit.Player = this;
            if (child.TryGetComponent(out IBuyer buyer)) _buyers.Add(buyer);
        }
    }

    private void Start()
    {
        Ruby = startedRubyCount;
    }

    private void OnEnable()
    {
        OnCheckDeselectUnit += TryDeselectUnit;
    }

    private void OnDisable()
    {
        OnCheckDeselectUnit -= TryDeselectUnit;
    }
    #endregion

    #region Get
    public PathPoint GetStartPathPoint() => startPathPoint;
    #endregion

    #region Select Units
    public void SelectUnit(ISelectableUnit selectableUnit)
    {
        if (_selectedUnit == selectableUnit) return;

        _selectedUnit = selectableUnit;
    }

    public void DeselectUnit(ISelectableUnit deselectedUnit)
    {
        if (deselectedUnit != _selectedUnit)
        {
            OnCheckDeselectUnit(deselectedUnit);
            return;
        }

        _selectedUnit = null;
    }

    public void TryDeselectUnit(ISelectableUnit deselectedUnit)
    {
        if (deselectedUnit == _selectedUnit) DeselectUnit(deselectedUnit);
    }
    #endregion

    #region Ruby
    public int Ruby
    {
        get => _ruby;
        private set
        {
            _ruby = value;
        }
    }

    public void AddRuby(int value)
    {
        Ruby += (int)(value * rubyModultiplier);

        TryBuyRandomPurchase();
    }

    private void TryBuyRandomPurchase()
    {
        IBuyer buyer = _buyers.Random();
        Purchase purchase = buyer.Purchases.Random();

        buyer.TryBuy(purchase, out string negativeResult);
    }
    #endregion

    #region Property
    public Material Material => material;

    public ISeller Seller => seller;
    #endregion
}
