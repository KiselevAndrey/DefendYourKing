using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    public Material material;

    [Header("References")]
    [SerializeField] private PathPoint startPathPoint;
    public MenuOfSelectedObject selectableMenu;

    #region Awake
    private void Awake()
    {
        foreach (IUnit child in transform.GetComponentsInChildren<IUnit>())
        {
            child.Player = this;
        }
    }
    #endregion

    #region Get
    public PathPoint GetStartPathPoint() => startPathPoint;
    #endregion
}
