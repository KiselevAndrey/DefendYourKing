using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PathPoint nextPathPoint;
    [SerializeField] private PathPoint prevPathPoint;
    [SerializeField] private Player playerToNextPoint;

    public PathPoint GetNextPlayerPathPoint(IPlayer unitPlayer)
    {
        if (unitPlayer == playerToNextPoint.GetComponent<IPlayer>())
            return nextPathPoint;
        else return prevPathPoint;
    }

    public Vector3 GetPosition() => transform.position;
}
