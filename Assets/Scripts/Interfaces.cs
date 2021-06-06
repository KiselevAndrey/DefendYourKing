using UnityEngine;

public interface IUnit
{
    int Health { get; set; }

    float BodyRadius { get; }

    Player Player { get; set; }

    void TakeDamage(int damage);

    void Death();

    void Select();

    void Deselect();

    Vector3 Position { get; }
}

public interface IAttack
{
    bool FindTarget();

    bool TryFindTarget();

    bool TryAttack();

    void Attack();

    bool CanAttack { get; }

    IUnit Target { get; set; }

    float Range { get; }

    int Damage { get; set; }

    bool CheckTheTarget { get; }
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