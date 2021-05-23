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
}

public interface IMob: IUnit, IAttack
{
    void Move();
}