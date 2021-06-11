using UnityEngine;

public class Mob : MonoBehaviour, IMob
{
    public enum States { FollowThePath, FollowToAttack, Attack, Stay }

    [Header("Parameters")]
    [SerializeField] private float bodyRadius;
    [SerializeField] private int maxHealth;
    public States startState;

    [Header("References")]
    public MobMove move;
    private IAttack attack;

    private States _currentStage;
    private IPlayer _player;
    private PathPoint _nextPathPoint;
    private int _currentHealth;
    private bool _isLife;

    #region Awake Update OnEnable OnDisable
    private void Awake()
    {
        attack = GetComponent<IAttack>();
    }

    private void Update()
    {
        UpdateStage();
    }

    private void OnEnable()
    {
        _currentStage = startState;
        Health = maxHealth;
        _isLife = true;
    }

    private void OnDisable()
    {
        attack.Target = null;
    }
    #endregion

    #region Stages
    private void UpdateStage()
    {
        switch (_currentStage)
        {
            case States.FollowThePath:
                if (_nextPathPoint)
                {
                    if (Vector3.Distance(Position, _nextPathPoint.GetPosition()) > BodyRadius)
                        move.MoveToTarget(_nextPathPoint.GetPosition());
                    else
                        _nextPathPoint = _nextPathPoint.GetNextPlayerPathPoint(Player);
                }

                attack.FindNearestTarget();
                break;

            case States.FollowToAttack:
                if (attack.Target.Health > 0)
                {
                    float maxDistance = Mathf.Max(BodyRadius, attack.Target.BodyRadius, attack.Range);
                    if (Vector3.Distance(Position, attack.Target.Position) > maxDistance && startState != States.Stay)
                        move.MoveToTarget(attack.Target.Position);
                    else
                    {
                        move.RotateToTarget(attack.Target.Position);
                        ChangeStage(States.Attack);
                    }
                }
                else
                    ChangeStage(startState);
                break;

            case States.Attack:
                attack.TryAttack();
                break;

            case States.Stay:
                attack.FindNearestTarget();
                break;
        }
    }

    public void ChangeStage(States newStage)
    {
        _currentStage = newStage;
    }

    public void ResetStage()
    {
        ChangeStage(startState);
    }
    #endregion

    #region Properties
    public float BodyRadius => bodyRadius;

    public IPlayer Player { get => _player; set => _player = value; }

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
    public void MoveToAttack()
    {
        ChangeStage(States.FollowToAttack);
    }

    public bool CanMove()
    {
        return startState != States.Stay;
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
