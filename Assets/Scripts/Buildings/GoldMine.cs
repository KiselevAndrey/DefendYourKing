using UnityEngine;

public class GoldMine : MonoBehaviour, IBuilding
{

    private Player _player;

    #region Property
    public Player Player { get => _player; set => _player = value; }
    #endregion

    #region Select
    public void Select()
    {
        SelectorOverlay.Instance.Show(this);
    }

    public void Deselect()
    {
        SelectorOverlay.Instance.Hide();
    }
    #endregion

    #region Get
    public Vector3 GetPosition()
    {
        return transform.position;
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
