using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Barrack : Building, IBuilding, ISelectableUnit
{
    [Header("Barrack Parameters")]
    [SerializeField] private float spawnCooldownTime;
    [SerializeField] private List<GameObject> spawnPrefabs;
    [SerializeField] private List<int> spawnCount;

    #region Start
    private new void Start()
    {
        while (spawnCount.Count < spawnPrefabs.Count)
            spawnCount.Add(0);

        base.Start();
    }
    #endregion

    public new void AfterBuilding()
    {
        Spawn();
    }

    #region Spawn
    private void Spawn()
    {
        StartCoroutine(Spawnind());
    }

    private IEnumerator Spawnind()
    {
        yield return new WaitForSeconds(spawnCooldownTime);

        StartCoroutine(Spawnind());

        for (int i = 0; i < spawnPrefabs.Count; i++)
        {
            for (int j = 0; j < spawnCount[i]; j++)
            {
                Lean.Pool.LeanPool.Spawn(spawnPrefabs[i], transform.position, transform.rotation).TryGetComponent(out IMob mob);
                mob.Player = Player;
                mob.PathPoint = Player.GetStartPathPoint();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private void AddSpawnCooldownTime(int addedCount, int addedPercent = 10)
    {
        for (int i = 0; i < addedCount; i++)
        {
            spawnCooldownTime += spawnCooldownTime * addedPercent / 100;
        }
    }
    #endregion

    #region Buyer
    public void BuyUnits(int unitIndex, int unitCount = 1)
    {
        if (unitIndex < spawnPrefabs.Count && unitIndex > -1)
        {
            spawnCount[unitIndex] += unitCount;
            AddSpawnCooldownTime(unitCount);
        }
    }
    #endregion
}
