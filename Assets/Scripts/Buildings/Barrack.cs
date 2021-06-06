using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Barrack : MonoBehaviour, IBuilding
{
    [Header("Parameters")]
    [SerializeField] float spawnCooldownTime;
    [SerializeField] List<GameObject> spawnPrefabs;
    [SerializeField] List<int> spawnCount;

    private Player _player;

    #region Start
    private void Start()
    {
        while (spawnCount.Count < spawnPrefabs.Count)
            spawnCount.Add(0);

        Spawn();
    }
    #endregion

    #region Property
    public Player Player { get => _player; set => _player = value; }
    public Vector3 Position => transform.position;
    #endregion

    #region Select
    public void Select()
    {
        SelectorOverlay.Instance.Show(this);
    }

    public void Deselect()
    {
        SelectorOverlay.Instance.Hide();
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

    #region Need Complete
    public int Health { get; set; }

    public float BodyRadius => throw new System.NotImplementedException();


    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }


    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }


    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
