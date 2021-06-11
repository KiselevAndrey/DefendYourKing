using System.Collections.Generic;
using UnityEngine;

public class RubyMine : Building, IBuilding, ISelectableUnit
{
    [Header("Mine Referencses")]
    [SerializeField] private List<Miner> miners;


}
