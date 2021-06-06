public class King : MobWithNavMesh, IMob
{
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

    #region Need complete
    public new void Deselect()
    {
    }

    public new void Select()
    {
        print("Select King");
    }
    #endregion
}
