using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PathPoint startPathPoint;

    #region Start
    private void Start()
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
