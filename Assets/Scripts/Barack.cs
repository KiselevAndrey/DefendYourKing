using UnityEngine;

public class Barack : MonoBehaviour, IBuilding
{
    public int Health { get => throw new System.NotImplementedException(); private set => throw new System.NotImplementedException(); }

    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        print(nameof(Select) + " " + gameObject.name);
        SelectorOverlay.Instance.Show(this);
    }

    public void Deselect()
    {
        print(nameof(Deselect) + " " + gameObject.name);
    }

    public void GetDamage(int damage)
    {
        throw new System.NotImplementedException();
    }


    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
