using UnityEngine;
using DG.Tweening;

public class Building : Unit, IBuilding, ISelectableUnit
{
    [Header("Buildings parameters")]
    [SerializeField] private float bodyRadius;
    [SerializeField] protected bool isBuild;
    [SerializeField] protected float buildTime;

    [Header("Buildings references")]
    [SerializeField] private Transform afterBuilding;

    protected IBuyer _buyer;
    protected BuildBuyer _buildBuyer;

    private ISeller _seller;
    private Vector3 _afterBuildingStartPosition;

    #region Awake Start OnEnable
    private void Awake()
    {
        _buyer = GetComponent<IBuyer>();
        _buildBuyer = GetComponent<BuildBuyer>();
    }

    protected void Start()
    {
        _afterBuildingStartPosition = afterBuilding.position;        
    }

    private new void OnEnable()
    {
        base.OnEnable();
        _isLife = isBuild;

        if (_isLife) Build();
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

        if (_isLife && _seller != null)
            _seller.Show(_buyer);
        else if (!_isLife)
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
        _isLife = true;
        Health = maxHealth;

        healthBar.gameObject.SetActive(true);
        afterBuilding.DOLocalMove(Vector3.zero, buildTime).OnComplete(() => animator.SetTrigger("Build"));        
    }

    protected void BeforeBuilding()
    {
        if (_isLife) return;

        animator.SetBool("Death", false);
        _isLife = false;
        Health = 0;

        _buyer.IsActive = _isLife;
        _buildBuyer.IsActive = !_isLife;
    }

    public void AfterBuilding()
    {
        _buyer.IsActive = _isLife;
        _buildBuyer.IsActive = !_isLife;
    }
    #endregion

    #region Die from animation
    public new void Destroy()
    {
        healthBar.gameObject.SetActive(false);
        Sequence dieSequence = DOTween.Sequence();
        dieSequence.Append(afterBuilding.DOMove(_afterBuildingStartPosition, 1f))
            .Join(afterBuilding.DOShakeRotation(1f, strength : 10))
            .OnComplete(() => animator.SetTrigger("Destroy"));
    }
    #endregion
}
