using UnityEngine;

#region IUnit
public interface IUnit
{
    int Health { get; set; }

    float BodyRadius { get; }

    IPlayer Player { get; set; }

    Vector3 Position { get; }
    
    int RewardForDeath { get; }

    void TakeDamage(int damage, IUnit attackedUnit);

    void Death();
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
    void Build();

    void AfterBuilding();
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

    System.Collections.Generic.List<Purchase> Purchases { get; }

    bool TryBuy(Purchase purshase, out string negativeResult);
}

public interface ISeller
{
    void Show(IBuyer buyer);

    void Hide();

    void TryBuy();
}
#endregion

#region Selectable
public interface ISelectable
{
    void Select();

    void Deselect();

    bool NeedHidePrevios { get; }
}

public interface ISelectableUnit: ISelectable
{
    Transform Transform { get; }
}
#endregion

#region IPlayer
public interface IPlayer
{
    Material Material { get; }

    ISeller Seller { get; }

    int Ruby { get; }

    void AddRuby(int value);

    void SpendRuby(int value);

    void TryDeselectUnit(ISelectableUnit deselectedUnit);

    void DeselectUnit(ISelectableUnit deselectedUnit);

    void SelectUnit(ISelectableUnit selectableUnit);

    PathPoint GetStartPathPoint();
}
#endregion