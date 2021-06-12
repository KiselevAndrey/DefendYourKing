using UnityEngine;

public class AttackBasic : MonoBehaviour, IAttack
{
    [Header("Find parameters")]
    [SerializeField] private float findRadius = 5f;
    [SerializeField] private int countOfSkipCallsFindTargetFunction = 30;

    [Header("Attack parameters")]
    [SerializeField] private float attackRadius = .3f;
    [SerializeField] private float cooldown = 1.5f;
    [SerializeField] private int damage;

    private IUnit unit;
    private IUnit _target;
    private int _countOfCallsFindTargetFunction;
    private bool _canAttack;

    #region Awake OnEnable
    private void Awake()
    {
        unit = GetComponent<IUnit>();
    }

    private void OnEnable()
    {
        _canAttack = true;
        _countOfCallsFindTargetFunction = 0;
    }
    #endregion

    #region Properties
    public IUnit Target { get => _target; set => _target = value; }

    public float Range { get => attackRadius; }

    public int Damage { get => damage; set => damage = value; }

    public bool CanAttack => _canAttack && CheckDistanceToTarget;

    public bool CheckDistanceToTarget => CheckTheTarget && Vector3.Distance(unit.Position, Target.Position) < Mathf.Max((unit.BodyRadius + Target.BodyRadius) * 1.1f, Range);

    public bool CheckTheTarget => Target != null && Target.Health > 0;
    #endregion

    #region Find Target
    public bool TryFindTarget()
    {
        if (Target != null) return true;

        if (_countOfCallsFindTargetFunction < countOfSkipCallsFindTargetFunction)
        {
            _countOfCallsFindTargetFunction++;
            return false;
        }

        _countOfCallsFindTargetFunction = 0;

        return FindNearestTarget();
    }

    public bool FindNearestTarget()
    {
        Target = null;

        // find all colliders
        Collider[] findColliders = Physics.OverlapSphere(transform.position, findRadius);
        System.Collections.Generic.List<IUnit> enemyUnits = new System.Collections.Generic.List<IUnit>();

        // if collider have IUnit, add to list
        for (int i = 0; i < findColliders.Length; i++)
        {
            if (findColliders[i].TryGetComponent(out IUnit unitEnemy) && unit.Player != unitEnemy.Player)
            {
                enemyUnits.Add(unitEnemy);
            }
        }

        // find nearest target
        float minDistance = findRadius;
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            float targetDistance = Vector3.Distance(unit.Position, enemyUnits[i].Position);
            if (targetDistance < minDistance)
            {
                Target = enemyUnits[i];
                minDistance = targetDistance;
            }
        }

        return Target != null;
    }
    #endregion

    #region Attack
    public bool TryAttack()
    {
        if (CanAttack)
        {
            StartCoroutine(Cooldown());
            return true;
        }

        CheckTarget();

        return false;
    }

    public void Attack()
    {
        Target?.TakeDamage((int)Random.Range(Damage * .7f, Damage * 1.3f), unit);

        CheckTarget();
    }

    private void CheckTarget()
    {
        if (Target != null && Target.Health <= 0)
        {
            Target.Death();
            Target = null;
        }
    }

    private System.Collections.IEnumerator Cooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(cooldown);
        _canAttack = true;
    }
    #endregion
}
