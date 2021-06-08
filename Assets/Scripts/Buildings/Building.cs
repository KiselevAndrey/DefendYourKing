using UnityEngine;

public class Building : MonoBehaviour, IBuilding, ISelectable
{
    protected Player _player;
    protected IBuyer _buyer;

    private void Awake()
    {
        _buyer = GetComponent<IBuyer>();
    }

    #region Property
    public Player Player { get => _player; set => _player = value; }
    public Vector3 Position => transform.position;
    #endregion

    #region Select
    public bool SelectAndDeselectPrevious()
    {
        SelectorOverlay.Instance.Show(_buyer);
        return true;
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
