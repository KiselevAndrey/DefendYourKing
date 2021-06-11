using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [Header("Parameters")]
    [SerializeField] private Material material;
    [SerializeField, Min(0)] private int startedRubyCount;

    [Header("References")]
    [SerializeField] private PathPoint startPathPoint;
    [SerializeField] private GameObject selectableMenuObject;
    [SerializeField] private StatsMenu statsMenu;
    [SerializeField] private King myKing;
    [SerializeField] private UnityEngine.UI.Text rubyText;

    public ISeller seller;

    public static Action<ISelectableUnit> OnCheckDeselectUnit;

    private ISelectableUnit _selectedUnit;
    private int _ruby;

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

    public void AddRuby(int income)
    {
        Ruby += income;
    }
    #endregion

    #region Property
    public Material Material => material;

    public ISeller Seller => seller;
    #endregion
}
