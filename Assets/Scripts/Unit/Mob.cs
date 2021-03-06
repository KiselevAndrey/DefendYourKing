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
    private Player _player;
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
                    if (Vector3.Distance(GetPosition(), _nextPathPoint.GetPosition()) > BodyRadius)
                        move.MoveToTarget(_nextPathPoint.GetPosition());
                    else
                        _nextPathPoint = _nextPathPoint.GetNextPlayerPathPoint(Player);
                }

                attack.FindTarget();
                break;

            case States.FollowToAttack:
                if (attack.Target.Health > 0)
                {
                    float maxDistance = Mathf.Max(BodyRadius, attack.Target.BodyRadius, attack.Range);
                    if (Vector3.Distance(GetPosition(), attack.Target.GetPosition()) > maxDistance && startState != States.Stay)
                        move.MoveToTarget(attack.Target.GetPosition());
                    else
                    {
                        move.RotateToTarget(attack.Target.GetPosition());
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
                attack.FindTarget();
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

    public Player Player { get => _player; set => _player = value; }

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
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        throw new System.NotImplementedException();
    }


    #endregion
}
