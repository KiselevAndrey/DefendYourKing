using System;
using UnityEngine;

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

    #region Awake Start OnEnable OnDisable
    private void Awake()
    {
        foreach (IUnit child in transform.GetComponentsInChildren<IUnit>())
        {
            child.Player = this;
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

    public void AddIncome(int income)
    {
        Ruby += (int)(income * rubyModultiplier);
    }
    #endregion

    #region Property
    public Material Material => material;

    public ISeller Seller => seller;
    #endregion
}
