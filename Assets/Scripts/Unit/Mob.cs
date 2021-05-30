using UnityEngine;

public class Mob : MonoBehaviour, IUnit
{
    enum Stages { FollowThePath, FollowToAttack, Attack }

    [Header("Parametrs")]
    [SerializeField] private PathPoint nextPathPoint;
    [SerializeField] private float bodyRadius;

    [Header("References")]
    public MobMove move;
    public IAttack attack;

    private Stages _currentStage;
    private Player _player;

    #region Awake Start Update
    private void Awake()
    {
        attack = GetComponent<IAttack>();
    }

    private void Start()
    {
        _currentStage = Stages.FollowThePath;
    }

    private void Update()
    {
        UpdateStage();
    }
    #endregion

    #region Stages
    private void UpdateStage()
    {
        switch (_currentStage)
        {
            case Stages.FollowThePath:
                if (nextPathPoint)
                {
                    if (Vector3.Distance(transform.position, nextPathPoint.GetPosition()) > BodyRadius)
                        move.MoveToTarget(nextPathPoint.GetPosition());
                    else
                        nextPathPoint = nextPathPoint.GetNextPlayerPathPoint(Player);
                }

                attack.FindTarget();
                break;

            case Stages.FollowToAttack:
                break;
            case Stages.Attack:
                break;
        }
    }
    #endregion

    #region Property
    public float BodyRadius => bodyRadius;

    public Player Player { get => _player; set => _player = value; }
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
