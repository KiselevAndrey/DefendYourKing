using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour, IBuilding
{
    #region Complete
    public void Select()
    {
        SelectorOverlay.Instance.Show(this);
    }

    public void Deselect()
    {
        SelectorOverlay.Instance.Hide();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    #endregion

    #region Need Complete
    public int Health { get; private set; }

    public float BodyRadius => throw new System.NotImplementedException();

    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }


    public void GetDamage(int damage)
    {
        throw new System.NotImplementedException();
    }


    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }

    public Player GetPlayer()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
