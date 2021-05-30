using UnityEngine;

public class Mob : MonoBehaviour, IUnit
{
    [Header("Parametrs")]
    [SerializeField] private Transform nextPathPoint;

    [Header("References")]
    public MobMove move;
    [SerializeField] private IAttack attack;

    #region Update
    private void Update()
    {
        move.MoveToTarget(nextPathPoint.position);
    }
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
