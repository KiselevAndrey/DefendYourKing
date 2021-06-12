using UnityEngine;

public class Building : Unit, IBuilding, ISelectableUnit
{
    [Header("Building parameters")]
    [SerializeField] private float bodyRadius;
    [SerializeField] private bool isBuild;

    protected IBuyer _buyer;
    protected BuildBuyer _buildBuyer;

    private ISeller _seller;

    #region Awake Start
    private void Awake()
    {
        _buyer = GetComponent<IBuyer>();
        _buildBuyer = GetComponent<BuildBuyer>();
    }

    protected void Start()
    {
        if (isBuild) Build();
    }
    #endregion

    #region Property
    public new float BodyRadius => bodyRadius;

    public new IPlayer Player
    {
        get => _player;
        set
        {
            _player = value;
            changedPlayerMaterial.material = value.Material;
            _seller = value.Seller;
        }
    }
    #endregion

    #region Select
    public new void Select()
    {
        base.Select();

        if (isBuild && _seller != null)
            _seller.Show(_buyer);
        else if (!isBuild)
            _seller.Show(_buildBuyer);
    }

    public new void Deselect()
    {
        if (_selected && _seller != null)
            _seller.Hide();

        base.Deselect();
    }
    #endregion

    #region Build
    public void Build()
    {
    }

    public void AfterBuilding()
    {
    }
    #endregion
}
