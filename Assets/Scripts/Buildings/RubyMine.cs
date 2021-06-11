using System.Collections.Generic;
using UnityEngine;

public class RubyMine : Building, IBuilding, ISelectableUnit
{
    [Header("Mine Parameters")]
    public int countMinerHitToExtract;
    public int income;

    [Header("Mine Referencses")]
    [SerializeField] private List<Miner> miners;

    private int _currentCountHitToExtract;

    private void Start()
    {
        for (int i = 0; i < miners.Count; i++)
        {
            miners[i].gameObject.SetActive(false);
        }

        AfterBuilding();
    }

    public new void AfterBuilding()
    {
        miners[0].gameObject.SetActive(true);
    }

    public void TryExtract()
    {
        if (_currentCountHitToExtract > 0) _currentCountHitToExtract--;
        else
        {
            _currentCountHitToExtract = countMinerHitToExtract;
            Player.AddIncome(income);
        }
    }
}
