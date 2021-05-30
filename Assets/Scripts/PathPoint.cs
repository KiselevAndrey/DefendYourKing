using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PathPoint nextPathPoint;
    [SerializeField] private PathPoint prevPathPoint;
    [SerializeField] private Player playerToNextPoint;

    public PathPoint GetNextPlayerPathPoint(Player unitPlayer)
    {
        if (unitPlayer == playerToNextPoint)
            return nextPathPoint;
        else return prevPathPoint;
    }

}
