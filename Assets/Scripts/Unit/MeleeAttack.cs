using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [Header("Parameters")]
    [SerializeField] private float findRadius = 5f;
    [SerializeField] private int countOfSkipCallsFindTargetFunction;

    [Header("References")]
    [SerializeField] private Mob mob;

    private IUnit _target;
    private int _countOfCallsFindTargetFunction;

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void FindTarget()
    {
        if(_countOfCallsFindTargetFunction < countOfSkipCallsFindTargetFunction)
        {
            _countOfCallsFindTargetFunction++;
        }
        else
        {
            _countOfCallsFindTargetFunction = 0;

            // find all colliders
            Collider[] findColliders = Physics.OverlapSphere(transform.position, findRadius);
            List<IUnit> units = new List<IUnit>();

            // if collider have IUnit, add to list
            for (int i = 0; i < findColliders.Length; i++)
            {
                if(findColliders[i].TryGetComponent(out IUnit unit))
                {
                    if (unit.Player != mob.Player) units.Add(unit);
                }
            }

            // find nearest target
            float minDistance = findRadius;
            for (int i = 0; i < units.Count; i++)
            {
                float targetDistance = Vector3.Distance(mob.GetPosition(), units[i].GetPosition());
                if (targetDistance < minDistance)
                {
                    _target = units[i];
                    minDistance = targetDistance;
                }
            }

            if (_target != null)
            {
                mob.ChangeStage(Mob.Stages.FollowToAttack);
            }
        }
    }
}
