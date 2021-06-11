using UnityEngine;

public class Building : Unit, IBuilding, ISelectableUnit
{
    [Header("Building parameters")]
    [SerializeField] private float bodyRadius;

    protected IBuyer _buyer;

    private ISeller _seller;

    #region Awake Start
    private void Awake()
    {
        _buyer = GetComponent<IBuyer>();
    }

    private void Start()
    {
        //_seller = SelectorOverlay.Instance;
    }
    #endregion

    #region Property
    public new float BodyRadius => bodyRadius;

    public new Player Player
    {
        get => _player;
        set
        {
            _player = value;
            changedPlayerMaterial.material = value.material;
            _seller = value.seller;
        }
    }
    #endregion

    #region Select
    public new void Select()
    {
        base.Select();

        _seller.Show(_buyer);
    }

    public new void Deselect()
    {
        base.Deselect();

        if (_selected)
        {
            _seller.Hide();
        }
    }
    #endregion

    #region Health
    public new void Death()
    {
        base.Death();

        Deselect();
    }
    #endregion

    #region Need Complete
    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
