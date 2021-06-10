using System;
using UnityEngine;
using UnityEngine.AI;

public class MobWithNavMesh : MonoBehaviour, IMob, ISelectableUnit
{
    public enum Stages { FollowThePath, FollowToAttack, Attack, Stay }

    [Header("Parameters")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Stages startStage;

    [Header("References")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected MeshRenderer changedPlayerMaterial;
    [SerializeField] protected UnitAnimatorsManager animatorsManager;
    [SerializeField] protected HealthBar healthBar;

    protected IAttack _attack;
    protected Stages _currentStage;
    protected Player _player;
    protected PathPoint _nextPathPoint;
    protected int _currentHealth;
    protected bool _isLife;
    protected float _bodyRadius;

    private bool _selected;

    #region Awake Update OnEnable OnDisable
    protected void Awake()
    {
        _attack = GetComponent<IAttack>();
        _bodyRadius = navMeshAgent.radius;
    }

    protected void Update()
    {
        UpdateStage();
    }

    protected void OnEnable()
    {
        startStage = Stages.FollowThePath;
        ChangeStage(startStage);

        healthBar.SetMaxHealt(maxHealth);
        Health = maxHealth;

        _isLife = true;
        navMeshAgent.avoidancePriority = UnityEngine.Random.Range(50, 100);
    }

    protected void OnDisable()
    {
        _attack.Target = null;
    }
    #endregion

    #region Stages
    protected void UpdateStage()
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
                    animatorsManager.StartMeleeAttackAnimation();

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
    public float BodyRadius => _bodyRadius;

    public Player Player 
    { 
        get => _player; 
        set 
        { 
            _player = value;
            changedPlayerMaterial.material = value.material;
        } 
    }

    public PathPoint PathPoint { get => _nextPathPoint; set => _nextPathPoint = value; }

    public Vector3 Position => transform.position;

    public int Health 
    { 
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Min(maxHealth, value);
            healthBar.SetHealth(_currentHealth);
        }
    }

    public bool NeedHidePrevios => true;

    public Transform Transform => transform;
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

        Deselect();
        
        _isLife = false;
        Lean.Pool.LeanPool.Despawn(gameObject);
    }
    #endregion

    #region Move
    protected void SetDestination(Vector3 target)
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
        if (_selected)
        {
            _player.DeselectUnit(this);
            _selected = false;
        }
    }

    public void Select()
    {
        _player.SelectUnit(this);
        _selected = true;
    }
    #endregion
}
