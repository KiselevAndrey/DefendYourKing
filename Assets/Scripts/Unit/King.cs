using UnityEngine;

public class King : MobWithNavMesh, IMob, ISelectable
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
    public new bool SelectAndDeselectPrevious()
    {
        SelectorOverlay.Instance.Show(buyer);
        return true;
    }

    public new void Deselect()
    {
        SelectorOverlay.Instance.Hide();
    }
    #endregion
}
