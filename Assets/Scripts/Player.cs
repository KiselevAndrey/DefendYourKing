using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    public Material material;

    [Header("References")]
    [SerializeField] private PathPoint startPathPoint;
    [SerializeField] private GameObject selectableMenuObject;
    
    public ISeller seller;

    #region Awake
    private void Awake()
    {
        if (selectableMenuObject)
            seller = selectableMenuObject.GetComponent<ISeller>();

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
