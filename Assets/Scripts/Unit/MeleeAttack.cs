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
    private bool _canAttack = true;

    #region Awake
    private void Awake()
    {
        mob = GetComponent<IMob>();
    }
    #endregion

    #region Properties
    public IUnit Target { get => _target; set => _target = value; }

    public float Range { get => attackRadius; }

    public int Damage { get => damage; set => damage = value; }
    #endregion

    #region Find Target
    public bool TryFindTarget()
    {
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
            float targetDistance = Vector3.Distance(mob.GetPosition(), units[i].GetPosition());
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
        if (_canAttack)
        {
            Target.TakeDamage((int)Random.Range(Damage * .7f, Damage * 1.3f));

            StartCoroutine(Cooldown());
        }

        if(Target.Health <= 0)
        {
            Target.Death();
            Target = null;
        }

        if (Target == null)
            mob.ResetStage();

        return false;
    }

    private System.Collections.IEnumerator Cooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(cooldown);
        _canAttack = true;
    }
    #endregion

}
