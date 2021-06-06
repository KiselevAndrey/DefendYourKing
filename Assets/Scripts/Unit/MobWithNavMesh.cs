using UnityEngine;
using UnityEngine.AI;

public class MobWithNavMesh : MonoBehaviour, IMob
{
    public enum States { FollowThePath, FollowToAttack, Attack, Stay }

    [Header("Parameters")]
    [SerializeField] private int maxHealth;
    public States startState;

    [Header("References")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private MeshRenderer changedPlayerMaterial;
    [SerializeField] private UnitAnimatorsManager animatorsManager;

    private IAttack _attack;
    private States _currentStage;
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
        ChangeStage(startState);
        Health = maxHealth;
        _isLife = true;
        navMeshAgent.avoidancePriority = Random.Range(1, 100);
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
            case States.FollowThePath:
                if (PathPoint)
                {
                    SetDestination(PathPoint.GetPosition());
                    if(Vector3.Distance(Position, PathPoint.GetPosition()) < BodyRadius * 5 && PathPoint.GetNextPlayerPathPoint(Player))
                    {
                        PathPoint = PathPoint.GetNextPlayerPathPoint(Player);
                    }
                }

                if (_attack.TryFindTarget())
                    ChangeStage(States.FollowToAttack);
                
                break;

            case States.FollowToAttack:
                if (_attack.Target.Health > 0)
                {
                    if (Vector3.Distance(Position, _attack.Target.Position) > navMeshAgent.stoppingDistance)
                    {
                        SetDestination(_attack.Target.Position);
                    }
                    else
                    {
                        ChangeStage(States.Attack);
                    }
                }
                else
                {
                    if (_attack.FindTarget())
                        ChangeStage(States.FollowToAttack);
                    else
                        ChangeStage(startState);
                }
                break;

            case States.Attack:
                RotateTowards(_attack.Target.Position);
                if (_attack.TryAttack())
                    animatorsManager.StartMeleeAttackAnimation();
                else
                    ResetStage();
                break;

            case States.Stay:
                if (_attack.TryFindTarget())
                {
                    if (_attack.CanAttack && Vector3.Distance(Position, _attack.Target.Position) > navMeshAgent.stoppingDistance)
                        ChangeStage(States.Attack);
                    else
                        RotateTowards(_attack.Target.Position);
                }
                break;
        }
    }

    public void ChangeStage(States newStage)
    {
        _currentStage = newStage;

        switch (newStage)
        {
            case States.FollowThePath:
                animatorsManager.IsStartMoveAnimation(true);
                navMeshAgent.stoppingDistance = BodyRadius;
                break;

            case States.FollowToAttack:
                animatorsManager.IsStartMoveAnimation(true);
                navMeshAgent.stoppingDistance = Mathf.Max(BodyRadius + _attack.Target.BodyRadius, _attack.Range);
                break;

            case States.Attack:
                animatorsManager.IsStartMoveAnimation(false);
                break;

            case States.Stay:
                animatorsManager.IsStartMoveAnimation(false);
                break;

            default:
                break;
        }
    }

    public void ResetStage()
    {
        ChangeStage(startState);
        _attack.FindTarget();
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
        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
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
