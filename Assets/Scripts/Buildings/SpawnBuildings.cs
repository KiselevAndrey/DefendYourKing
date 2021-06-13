using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildings : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Transform> buildPositions;
    [SerializeField] private GameObject generalBuildingPrefab;

    private void Start()
    {
        for (int i = 0; i < buildPositions.Count; i++)
        {
            Lean.Pool.LeanPool.Spawn(generalBuildingPrefab, buildPositions[i]);
        }
    }
}
