using UnityEngine;

public class Mob : MonoBehaviour, IUnit
{
    enum Stages { FollowThePath, FollowToAttack, Attack }

    [Header("Parametrs")]
    [SerializeField] private Transform nextPathPoint;
    [SerializeField] private float bodyRadius;

    [Header("References")]
    public MobMove move;
    public IAttack attack;

    private Stages _currentStage;

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
                if (nextPathPoint && Vector3.Distance(transform.position, nextPathPoint.position) > BodyRadius)
                {
                    move.MoveToTarget(nextPathPoint.position);
                }
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
    public Player Player { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
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
