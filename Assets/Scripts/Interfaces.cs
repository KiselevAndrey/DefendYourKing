using UnityEngine;

public interface IUnit
{
    int Health { get; }

    float BodyRadius { get; }

    Player Player { get; set; }

    void GetDamage(int damage);

    void Death();

    void Select();

    void Deselect();

    Vector3 GetPosition();
}

public interface IAttack
{
    void FindTarget();

    void Attack();

    IUnit Target { get; }

    float Range { get; }
}

public interface IBuilding : IUnit
{
    void Upgrade();

    void Build();
}

public interface IMob: IUnit
{
    PathPoint PathPoint { get; set; }
}