using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildings : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Transform> buildPositions;
    [SerializeField] private GameObject generalBuildingPrefab;

    private IPlayer _player;

    private void Awake()
    {
        _player = GetComponentInParent<IPlayer>();
    }

    private void Start()
    {
        for (int i = 0; i < buildPositions.Count; i++)
        {
            if(Lean.Pool.LeanPool.Spawn(generalBuildingPrefab, buildPositions[i]).TryGetComponent(out GeneralBuilding building))
            {
                building.Player = _player;
            }
        }
    }
}
