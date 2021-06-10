using UnityEngine;

public class King : MobWithNavMesh, ISelectableUnit
{
    [Header("King Reference")]
    [SerializeField] private Buyer buyer;

    #region OnEnable
    protected new void OnEnable()
    {
        base.OnEnable();

        navMeshAgent.avoidancePriority = 0;
        startStage = Stages.Stay;
        ChangeStage(startStage);
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
}
