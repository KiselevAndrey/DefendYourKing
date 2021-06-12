using System.Collections.Generic;
using UnityEngine;

public class RubyMine : Building, IBuilding, ISelectableUnit
{
    [Header("Mine Parameters")]
    [SerializeField] private int countMinerHitToExtract;
    [SerializeField] private int income;
    [SerializeField, Min(1)] private float multiplierIncome;

    [Header("Mine Referencses")]
    [SerializeField] private List<Miner> miners;

    private int _currentCountHitToExtract;
    private int _activeMiners;

    private new void Start()
    {
        base.Start();

        for (int i = 0; i < miners.Count; i++)
        {
            miners[i].gameObject.SetActive(false);
        }
    }

    public new void AfterBuilding()
    {
        AddMiner();
    }

    public void TryExtract()
    {
        if (_currentCountHitToExtract < countMinerHitToExtract) _currentCountHitToExtract++;
        else
        {
            _currentCountHitToExtract = 0;
            Player.AddRuby(income);
        }
    }

    #region Buyer
    public void AddMiner()
    {
        if (_activeMiners < miners.Count)
        {
            miners[_activeMiners].gameObject.SetActive(true);
            _activeMiners++;
        }
    }

    public void AddIncome()
    {
        income = (int)(income * multiplierIncome);
    }

    public void LessHitToIncome()
    {
        countMinerHitToExtract--;
    }
    #endregion
}
