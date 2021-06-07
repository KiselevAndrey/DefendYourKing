using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Barrack : Building, IBuilding
{
    [Header("Barrack Parameters")]
    [SerializeField] float spawnCooldownTime;
    [SerializeField] List<GameObject> spawnPrefabs;
    [SerializeField] List<int> spawnCount;

    #region Start
    private void Start()
    {
        while (spawnCount.Count < spawnPrefabs.Count)
            spawnCount.Add(0);

        Spawn();
    }
    #endregion

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
    #endregion
}
