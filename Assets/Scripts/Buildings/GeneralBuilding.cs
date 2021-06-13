using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBuilding : MonoBehaviour, ISelectableUnit
{
    [Header("References")]
    [SerializeField] private Transform afterBuilding;
    [SerializeField] private BuildingSoundsManager buildingSoundsManager;
    [SerializeField] private BuildBuyerNew buildBuyer;
    [SerializeField] private List<int> intd;

    private IPlayer _player;
    private ISeller _seller;
    private IBuilding _constructedBuilding;
    private IBuyer _buyer;
    private Vector3 _afterBuildingStartPosition;
    private bool _selected;

    private void Start()
    {
        _afterBuildingStartPosition = afterBuilding.position;
    }

    #region Properties
    public IPlayer Player
    {
        get => _player;
        set
        {
            _player = value;
            _seller = value.Seller;
        }
    }

    public Transform Transform => transform;

    public bool NeedHidePrevios => true;

    private bool IsLife => _constructedBuilding != null;
    #endregion

    #region Select
    public void Select()
    {
        _selected = true;
        Player.SelectUnit(this);

        if (IsLife && _seller != null)
            _seller.Show(_buyer);
        else if (!IsLife)
            _seller.Show(buildBuyer);
    }

    public void Deselect()
    {
        if (_selected)
        {
            if (_seller != null) _seller.Hide();

            Player.DeselectUnit(this);
            _selected = false;
        }
    }
    #endregion
}
