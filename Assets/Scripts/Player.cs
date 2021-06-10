using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    public Material material;

    [Header("References")]
    [SerializeField] private PathPoint startPathPoint;
    [SerializeField] private GameObject selectableMenuObject;
    [SerializeField] private StatsMenu statsMenu;
    [SerializeField] private King myKing;
    
    public ISeller seller;

    public static Action<ISelectableUnit> OnCheckDeselectUnit;

    private ISelectableUnit _selectedUnit;

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

    private void TryDeselectUnit(ISelectableUnit deselectedUnit)
    {
        if (deselectedUnit == _selectedUnit) DeselectUnit(deselectedUnit);
    }
    #endregion
}
