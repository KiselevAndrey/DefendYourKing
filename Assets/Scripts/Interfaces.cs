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

    Purchase[] Purshases { get; }

    bool TryBuy(Purchase purshase, ref string negativeResult);
}

public interface ISeller
{
    void Show(IBuyer buyer);

    void Hide();

    void TryBuy();
}
#endregion

public interface ISelectable
{
    void Select();

    void Deselect();

    bool NeedHidePrevios { get; }
}