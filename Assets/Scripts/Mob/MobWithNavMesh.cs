using UnityEngine;
using UnityEngine.AI;

public class MobWithNavMesh : Unit, IMob, ISelectableUnit
{
    protected enum Stages { FollowThePath, FollowToAttack, Attack, Stay }

    [Header("Mob Parameters")]
    [SerializeField] protected Stages startStage;
    [SerializeField] private bool printNow;

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
        if (!_isLife) return;

        UpdateStage();
    }

    protected new void OnEnable()
    {
        base.OnEnable();

        startStage = Stages.FollowThePath;
        ChangeStage(startStage);

        navMeshAgent.enabled = true;
        navMeshAgent.avoidancePriority = Random.Range(50, 100);
    }

    private void OnDisable()
    {
        _attack.Target = null;
    }
    #endregion

    #region Stages
    #region UpdateStage
    private void UpdateStage()
    {
        switch (_currentStage)
        {
            case Stages.FollowThePath:
                UpdateStagesFollowThePath();
                break;

            case Stages.FollowToAttack:
                UpdateStagesFollowToAttack();
                break;

            case Stages.Attack:
                UpdateStagesAttack();
                break;

            case Stages.Stay:
                UpdateStagesStay();
                break;
        }
    }

    private void UpdateStagesFollowThePath()
    {
        if (PathPoint)
        {
            SetDestination(PathPoint.GetPosition());
            if (printNow) print(Vector3.Distance(Position, PathPoint.GetPosition()) + " " + BodyRadius * 5f);
            if (Vector3.Distance(Position, PathPoint.GetPosition()) < BodyRadius * 5f && PathPoint.GetNextPlayerPathPoint(Player))
            {
                PathPoint = PathPoint.GetNextPlayerPathPoint(Player);
            }
        }

        if (_attack.TryFindTarget())
            ChangeStage(Stages.FollowToAttack);
    }

    private void UpdateStagesFollowToAttack()
    {
        // if have life target
        if (_attack.CheckTheTarget)
        {
            if (printNow) print("Find Target");

            if(!_attack.CheckDistanceToTarget)
                SetDestination(_attack.Target.Position);
            else
                ChangeStage(Stages.Attack);
        }
        else
        {
            if (printNow) print("Dont Find Target");
            if (_attack.FindNearestTarget())
            {
                if (printNow) print("Find Nearest Target");
                ChangeStage(Stages.FollowToAttack);
            }
            else
            {
                if (printNow) print("Walk to startStage");
                ChangeStage(startStage);
            }
        }
    }
    
    private void UpdateStagesAttack()
    {
        if (!_attack.CheckDistanceToTarget)
        {
            if (printNow) print("Reset Stage");
            ResetStage();
            return;
        }

        RotateTowards(_attack.Target.Position);

        if (_attack.TryAttack())
            StartAttackAnimation();
    }

    private void UpdateStagesStay()
    {
        if (_attack.TryFindTarget())
        {
            if (printNow) print("FindTargete");
            if (_attack.CanAttack)
            {
                if (printNow) print("CanAttack");
                ChangeStage(Stages.Attack);
            }
            else if (!_attack.CheckTheTarget)
            {
                if (printNow) print("NoTarget");
                _attack.Target = null;
            }
        }
    }
    #endregion

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

    #region Move
    private void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 endRotation = target - Position;
        if (printNow) print(endRotation);
        if (endRotation != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, endRotation, Time.deltaTime * 10f);
        }
    }
    #endregion

    #region Animator
    protected void StartAttackAnimation() => animatorsManager.StartMeleeAttackAnimation();
    #endregion

    #region Die
    public new void Death()
    {
        if (!_isLife) return;
        base.Death();

        navMeshAgent.enabled = false;
    }

    public new void Destroy()
    {
        if (printNow) print(name + " Destroy");
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
    #endregion
}
