using UnityEngine;

#region IUnit
public interface IUnit
{
    int Health { get; set; }

    float BodyRadius { get; }

    Player Player { get; set; }

    void TakeDamage(int damage);

    void Death();

    Vector3 Position { get; }
}

public interface IAttack
{
    bool FindNearestTarget();

    bool TryFindTarget();

    bool TryAttack();

    void Attack();

    bool CanAttack { get; }

    IUnit Target { get; set; }

    float Range { get; }

    int Damage { get; set; }

    bool CheckTheTarget { get; }

    bool CheckDistanceToTarget { get; }
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
#endregion

#region IPurchases
public interface IBuyer
{
    int MaxCountPurchases { get; }

    Vector3 Position { get; }

    Purshase[] Purshases { get; }

    void TryBuy(Purshase purshase);
}
#endregion

public interface ISelectable
{
    /// <summary>
    /// Made a selection and returns whether to hide the previous selection.
    /// Выполнить выбор и вернуть надо ли прятать предыдущий выбор
    /// </summary>
    /// <returns></returns>
    bool SelectAndDeselectPrevious();

    void Deselect();
}