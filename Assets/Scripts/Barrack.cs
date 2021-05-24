using UnityEngine;

public class Barrack : MonoBehaviour, IBuilding
{
    public int Health { get; private set; }

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
        SelectorOverlay.Instance.Show(this);
    }

    public void Deselect()
    {
        SelectorOverlay.Instance.Hide();
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
