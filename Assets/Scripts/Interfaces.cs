using UnityEngine;

public interface IUnit
{
    int Health { get; }

    float BodyRadius { get; }

    void GetDamage(int damage);

    void Death();

    void Select();

    void Deselect();

    Player GetPlayer();
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

public interface IMob: IUnit
{
    void Move();
}