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

    Vector3 GetPosition();
}

public interface IAttack
{
    void FindTarget();

    bool TryFindTarget();

    bool TryAttack();

    IUnit Target { get; set; }

    float Range { get; }

    int Damage { get; set; }
}

public interface IBuilding : IUnit
{
    void Upgrade();

    void Build();
}

public interface IMob: IUnit
{
    PathPoint PathPoint { get; set; }

    void ResetStage();

    void MoveToAttack();

    bool CanMove();
}