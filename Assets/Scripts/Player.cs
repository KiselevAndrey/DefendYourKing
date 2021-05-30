using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PathPoint startPathPoint;

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
