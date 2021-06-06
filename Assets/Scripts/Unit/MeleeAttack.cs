using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [Header("Find parameters")]
    [SerializeField] private float findRadius = 5f;
    [SerializeField] private int countOfSkipCallsFindTargetFunction = 30;

    [Header("Attack parameters")]
    [SerializeField] private float attackRadius = .3f;
    [SerializeField] private float cooldown = 1.5f;
    [SerializeField] private int damage;

    [Header("References")]
    [SerializeField] private IMob mob;

    private IUnit _target;
    private int _countOfCallsFindTargetFunction;
    private bool _canAttack;

    #region Awake OnEnable
    private void Awake()
    {
        mob = GetComponent<IMob>();
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

    public bool CanAttack => _canAttack && CheckTheTarget;

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

        return FindTarget();
    }

    public bool FindTarget()
    {
        // find all colliders
        Collider[] findColliders = Physics.OverlapSphere(transform.position, findRadius);
        System.Collections.Generic.List<IUnit> units = new System.Collections.Generic.List<IUnit>();

        // if collider have IUnit, add to list
        for (int i = 0; i < findColliders.Length; i++)
        {
            if (findColliders[i].TryGetComponent(out IUnit unit) && unit.Player != mob.Player)
            {
                units.Add(unit);
            }
        }

        // find nearest target
        float minDistance = findRadius;
        for (int i = 0; i < units.Count; i++)
        {
            float targetDistance = Vector3.Distance(mob.Position, units[i].Position);
            if (targetDistance < minDistance)
            {
                Target = units[i];
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
        Target?.TakeDamage((int)Random.Range(Damage * .7f, Damage * 1.3f));

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
