using UnityEngine;

public class King : MobWithNavMesh, IMob
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
        SelectorOverlay.Instance.Show(buyer);
    }

    public new void Deselect()
    {
        SelectorOverlay.Instance.Hide();
    }
    #endregion
}
