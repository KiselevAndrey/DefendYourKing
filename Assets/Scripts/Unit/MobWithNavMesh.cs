using UnityEngine;
using UnityEngine.AI;

public class MobWithNavMesh : Unit, IMob, ISelectableUnit
{
    protected enum Stages { FollowThePath, FollowToAttack, Attack, Stay }

    [Header("Mob Parameters")]
    [SerializeField] protected Stages startStage;

    [Header("Mob References")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] private UnitAnimatorsManager animatorsManager;

    private IAttack _attack;
    protected Stages _currentStage;
    protected PathPoint _nextPathPoint;

    #region Awake Update OnEnable OnDisable
    private void Awake()
    {
        _attack = GetComponent<IAttack>();
        _bodyRadius = navMeshAgent.radius;
    }

    private void Update()
    {
        UpdateStage();
    }

    protected new void OnEnable()
    {
        base.OnEnable();

        startStage = Stages.FollowThePath;
        ChangeStage(startStage);

        navMeshAgent.avoidancePriority = Random.Range(50, 100);
    }

    private void OnDisable()
    {
        _attack.Target = null;
    }
    #endregion

    #region Stages
    private void UpdateStage()
    {
        switch (_currentStage)
        {
            case Stages.FollowThePath:
                if (PathPoint)
                {
                    SetDestination(PathPoint.GetPosition());
                    if(Vector3.Distance(Position, PathPoint.GetPosition()) < BodyRadius * 5 && PathPoint.GetNextPlayerPathPoint(Player))
                    {
                        PathPoint = PathPoint.GetNextPlayerPathPoint(Player);
                    }
                }

                if (_attack.TryFindTarget())
                    ChangeStage(Stages.FollowToAttack);
                
                break;

            case Stages.FollowToAttack:
                if (_attack.CheckTheTarget)
                {
                    if (Vector3.Distance(Position, _attack.Target.Position) > navMeshAgent.stoppingDistance)
                    {
                        SetDestination(_attack.Target.Position);
                    }
                    else
                    {
                        ChangeStage(Stages.Attack);
                    }
                }
                else
                {
                    if (_attack.FindNearestTarget())
                        ChangeStage(Stages.FollowToAttack);
                    else
                        ChangeStage(startStage);
                }
                break;

            case Stages.Attack:
                if (!_attack.CheckDistanceToTarget)
                {
                    ResetStage();
                    break;
                }

                RotateTowards(_attack.Target.Position);

                if (_attack.TryAttack())
                    StartAttackAnimation();

                break;

            case Stages.Stay:
                if (_attack.TryFindTarget())
                {
                    if (_attack.CanAttack)
                    {
                        ChangeStage(Stages.Attack);
                    }
                    else if (!_attack.CheckTheTarget)
                        _attack.Target = null;
                    else
                        RotateTowards(_attack.Target.Position);
                }
                break;
        }
    }

    protected void ChangeStage(Stages newStage)
    {
        _currentStage = newStage;

        switch (newStage)
        {
            case Stages.FollowThePath:
                animatorsManager.IsStartMoveAnimation(true);
                navMeshAgent.stoppingDistance = BodyRadius;
                break;

            case Stages.FollowToAttack:
                animatorsManager.IsStartMoveAnimation(true);
                navMeshAgent.stoppingDistance = Mathf.Max((BodyRadius + _attack.Target.BodyRadius) * 1.1f, _attack.Range);
                break;

            case Stages.Attack:
                animatorsManager.IsStartMoveAnimation(false);
                break;

            case Stages.Stay:
                animatorsManager.IsStartMoveAnimation(false);
                break;
        }
    }

    private void ResetStage()
    {
        if (_attack.FindNearestTarget())
        {
            switch (startStage)
            {                 
                case Stages.Stay:
                    ChangeStage(Stages.Stay);
                    break;

                case Stages.FollowThePath:
                default:
                    ChangeStage(Stages.FollowToAttack);
                    break;
            }
        }
        else
            ChangeStage(startStage);

    }
    #endregion

    #region Properties
    public PathPoint PathPoint { get => _nextPathPoint; set => _nextPathPoint = value; }
    #endregion

    #region Health
    public new void Death()
    {
        base.Death();

        Deselect();
    }
    #endregion

    #region Move
    private void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = target - Position;
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }
    #endregion

    #region Animator
    protected void StartAttackAnimation() => animatorsManager.StartMeleeAttackAnimation();
    #endregion
}
