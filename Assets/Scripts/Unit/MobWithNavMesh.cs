using UnityEngine;
using UnityEngine.AI;

public class MobWithNavMesh : MonoBehaviour, IMob
{
    public enum Stages { FollowThePath, FollowToAttack, Attack, Stay }

    [Header("Parameters")]
    [SerializeField] private int maxHealth;
    public Stages startStage;

    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private MeshRenderer changedPlayerMaterial;
    [SerializeField] private UnitAnimatorsManager animatorsManager;

    private IAttack _attack;
    private Stages _currentStage;
    private Player _player;
    private PathPoint _nextPathPoint;
    private int _currentHealth;
    private bool _isLife;
    private float _bodyRadius;

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

    private void OnEnable()
    {
        ChangeStage(startStage);
        Health = maxHealth;
        _isLife = true;
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
                if (!_attack.CheckTheTarget)
                {
                    ResetStage();
                    break;
                }

                RotateTowards(_attack.Target.Position);

                if (_attack.TryAttack())
                    animatorsManager.StartMeleeAttackAnimation();
                else if (startStage == Stages.Stay || !_attack.CheckDistanceToTarget)
                {
                    ChangeStage(startStage);
                    break;
                }

                break;

            case Stages.Stay:
                if (_attack.TryFindTarget())
                {                    
                    if (_attack.CanAttack)
                        ChangeStage(Stages.Attack);
                    else
                        RotateTowards(_attack.Target.Position);
                }
                break;
        }
    }

    public void ChangeStage(Stages newStage)
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

    public void ResetStage()
    {
        ChangeStage(startStage);
        _attack.FindNearestTarget();
    }
    #endregion

    #region Properties
    public float BodyRadius => _bodyRadius;

    public Player Player { get => _player; 
        set 
        { 
            _player = value;
            changedPlayerMaterial.material = value.material;
        } 
    }

    public PathPoint PathPoint { get => _nextPathPoint; set => _nextPathPoint = value; }

    public Vector3 Position => transform.position;

    public int Health { get => _currentHealth; set => _currentHealth = Mathf.Min(maxHealth, value); }
    #endregion

    #region Health
    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0) Death();
    }

    public void Death()
    {
        if (!_isLife) return;

        _isLife = false;
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
    #endregion

    #region Move
    public void SetDestination(Vector3 target)
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

    #region Need complete
        public void Deselect()
    {
    }

    public void Select()
    {
    }
    #endregion
}
