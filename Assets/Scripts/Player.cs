using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [Header("Player Parameters")]
    [SerializeField] protected Material material;
    [SerializeField, Min(0)] protected int startedRubyCount;

    [Header("References")]
    [SerializeField] protected PathPoint startPathPoint;
    [SerializeField] private GameObject selectableMenuObject;
    [SerializeField] private StatsMenu statsMenu;
    [SerializeField] private King myKing;
    [SerializeField] private UnityEngine.UI.Text rubyText;

    private ISeller seller;

    public static Action<ISelectableUnit> OnCheckDeselectUnit;

    protected ISelectableUnit _selectedUnit;
    protected int _ruby;

    #region Awake Start OnEnable OnDisable
    private void Awake()
    {
        if (selectableMenuObject)
            seller = selectableMenuObject.GetComponent<ISeller>();

        foreach (IUnit child in transform.GetComponentsInChildren<IUnit>())
        {
            child.Player = this;
        }
    }

    private void Start()
    {
        if(statsMenu)
            myKing.Select();

        Ruby = startedRubyCount;
    }

    protected void OnEnable()
    {
        OnCheckDeselectUnit += TryDeselectUnit;
    }

    protected void OnDisable()
    {
        OnCheckDeselectUnit -= TryDeselectUnit;
    }
    #endregion

    #region Get
    public PathPoint GetStartPathPoint() => startPathPoint;
    #endregion

    #region Select Units
    public void SelectKing()
    {
        myKing.Select();
    }

    public void SelectUnit(ISelectableUnit selectableUnit)
    {
        if (_selectedUnit == selectableUnit) return;

        statsMenu.SelectUnit(selectableUnit);
        _selectedUnit = selectableUnit;
    }

    public void DeselectUnit(ISelectableUnit deselectedUnit)
    {
        if (deselectedUnit != _selectedUnit)
        {
            OnCheckDeselectUnit(deselectedUnit);
            return;
        }

        myKing.Select();
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
            rubyText.text = value.ToString();
        }
    }

    public void AddRuby(int value)
    {
        Ruby += value;
    }

    public void SpendRuby (int value)
    {
        Ruby -= value;
    }
    #endregion

    #region Property
    public Material Material => material;

    public ISeller Seller => seller;
    #endregion
}
