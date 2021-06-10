using UnityEngine;

public class Building : MonoBehaviour, IBuilding, ISelectable
{
    protected Player _player;
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
    public Player Player 
    { 
        get => _player;
        set
        {
            _player = value;
            _seller = value.seller;
        }
    }

    public Vector3 Position => transform.position;

    public bool NeedHidePrevios => true;
    #endregion

    #region Select
    public void Select()
    {
        _seller.Show(_buyer);
    }

    public void Deselect()
    {
        _seller.Hide();
    }
    #endregion

    #region Need Complete
    public int Health { get; set; }

    public float BodyRadius => throw new System.NotImplementedException();


    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
