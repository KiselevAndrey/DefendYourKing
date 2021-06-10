using UnityEngine;

public class King : MobWithNavMesh, IMob, ISelectableUnit
{
    [Header("King Reference")]
    [SerializeField] private Buyer buyer;

    #region Update OnEnable
    protected new void Update()
    {
        UpdateStage();
    }

    protected new void OnEnable()
    {
        startStage = Stages.Stay;
        ChangeStage(startStage);

        healthBar.SetMaxHealt(maxHealth);
        Health = maxHealth;

        _isLife = true;
    }
    #endregion

    #region Select Deselect
    public new void Select()
    {
        _player.SelectUnit(this);
    }
    #endregion
}
