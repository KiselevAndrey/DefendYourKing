using UnityEngine;

public interface IUnit
{
    void GetDamage(int damage);

    void Death();

    void Select();

    void Deselect();
}

public interface IAttack
{
    void FindTarget();

    void Attack();
}

public interface IBuilding : IUnit
{
    void Upgrade();

    void Build();

    Vector3 GetPosition();
}

public interface IMob: IUnit, IAttack
{
    void Move();
}