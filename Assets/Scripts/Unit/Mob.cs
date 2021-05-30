using UnityEngine;

public class Mob : MonoBehaviour, IMob
{
    public enum Stages { FollowThePath, FollowToAttack, Attack }

    [Header("Parameters")]
    [SerializeField] private float bodyRadius;

    [Header("References")]
    public MobMove move;
    public IAttack attack;


    private Stages _currentStage;
    private Player _player;
    private PathPoint _nextPathPoint;

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
        _currentStage = Stages.FollowThePath;
    }

    private void OnDisable()
    {
        
    }
    #endregion

    #region Stages
    private void UpdateStage()
    {
        switch (_currentStage)
        {
            case Stages.FollowThePath:
                if (_nextPathPoint)
                {
                    if (Vector3.Distance(GetPosition(), _nextPathPoint.GetPosition()) > BodyRadius)
                        move.MoveToTarget(_nextPathPoint.GetPosition());
                    else
                        _nextPathPoint = _nextPathPoint.GetNextPlayerPathPoint(Player);
                }

                attack.FindTarget();
                break;

            case Stages.FollowToAttack:
                float maxDistance = Mathf.Max(BodyRadius, attack.Target.BodyRadius, attack.Range);
                if (Vector3.Distance(GetPosition(), attack.Target.GetPosition()) > maxDistance)
                    move.MoveToTarget(attack.Target.GetPosition());
                else
                {
                    move.RotateToTarget(attack.Target.GetPosition());
                    ChangeStage(Stages.Attack);
                }
                break;

            case Stages.Attack:
                break;
        }
    }

    public void ChangeStage(Stages newStage)
    {
        _currentStage = newStage;
    }
    #endregion

    #region Property
    public float BodyRadius => bodyRadius;

    public Player Player { get => _player; set => _player = value; }

    public PathPoint PathPoint { get => _nextPathPoint; set => _nextPathPoint = value; }
    #endregion

    #region Get
    public Vector3 GetPosition() => transform.position;
    #endregion

    #region Need complete
    public int Health => throw new System.NotImplementedException();


    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Deselect()
    {
        throw new System.NotImplementedException();
    }

    public void GetDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
