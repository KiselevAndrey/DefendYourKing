using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{
    [Header("Reference")]
    [SerializeField] protected Buyer buyer;

    protected Player _player;

    #region Property
    public Player Player { get => _player; set => _player = value; }
    public Vector3 Position => transform.position;
    #endregion

    #region Select
    public void Select()
    {
        SelectorOverlay.Instance.Show(buyer);
    }

    public void Deselect()
    {
        SelectorOverlay.Instance.Hide();
    }
    #endregion

    #region Need Complete
    public int Health { get; set; }

    public float BodyRadius => throw new System.NotImplementedException();

    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }


    public void TakeDamage(int damage)
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
