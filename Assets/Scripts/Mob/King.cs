using UnityEngine;
using UnityEngine.UI;

public class King : MobWithNavMesh, ISelectableUnit, IUnit
{
    [Header("King Reference")]
    [SerializeField] private Buyer buyer;
    [SerializeField] private Image fillingHealthImage;
    [SerializeField] private Text healthText;

    #region OnEnable
    protected new void OnEnable()
    {
        base.OnEnable();

        navMeshAgent.avoidancePriority = 0;
        startStage = Stages.Stay;
        ChangeStage(startStage);

        SetGUIHealthBar();
    }

    private void Start()
    {
        PathPoint = _player.GetStartPathPoint();
    }
    #endregion

    #region Select Deselect
    public new void Select()
    {
        base.Select();
    }
    #endregion

    #region Health
    private void SetGUIHealthBar()
    {
        if (fillingHealthImage && healthText)
        {
            fillingHealthImage.fillAmount = (float)Health / maxHealth;
            healthText.text = Health.ToString() + " / " + maxHealth.ToString() + " HP";
        }
    }

    public new void TakeDamage(int damage, IUnit attackedUnit)
    {
        base.TakeDamage(damage, attackedUnit);

        SetGUIHealthBar();
    }

    public new  void Destroy()
    {
        print("King is Dead");
    }
    #endregion

}
