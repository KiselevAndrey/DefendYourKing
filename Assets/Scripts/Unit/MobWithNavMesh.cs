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
                    if (Vector3.Distance(GetPosition(), PathPoint.GetPosition()) < BodyRadius * 5 && PathPoint.GetNextPlayerPathPoint(Player))
                    {
                        PathPoint = PathPoint.GetNextPlayerPathPoint(Player);
                        SetDestination(PathPoint.GetPosition());
                    }
                    SetDestination(PathPoint.GetPosition());
                }

                _attack.FindTarget();
                break;

            case States.FollowToAttack:
                if (_attack.Target.Health > 0)
                {
                    navMeshAgent.stoppingDistance = Mathf.Max(BodyRadius, _attack.Target.BodyRadius, _attack.Range);
                    if (Vector3.Distance(GetPosition(), _attack.Target.GetPosition()) > navMeshAgent.stoppingDistance && startState != States.Stay)
                        SetDestination(_attack.Target.GetPosition());
                    else
                    {
                        //move.RotateToTarget(attack.Target.GetPosition());
                        //transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
                        //if (!navMeshAgent.updateRotation) SetDestination(attack.Target.GetPosition());
                        RotateTowards(_attack.Target.GetPosition());
                        ChangeStage(States.Attack);
                    }
                }
                else
                    ChangeStage(startState);
                break;

            case States.Attack:
                _attack.TryAttack();
                break;

            case States.Stay:
                _attack.FindTarget();
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
                break;

            case States.FollowToAttack:
                animatorsManager.IsStartMoveAnimation(true);
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

    public int Health { get => _currentHealth; set => _currentHealth = Mathf.Min(maxHealth, value); }
    #endregion

    #region Get
    public Vector3 GetPosition() => transform.position;
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

    public void MoveToAttack()
    {
        ChangeStage(States.FollowToAttack);
    }

    public bool CanMove()
    {
        return startState != States.Stay;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
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
