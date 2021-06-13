using System.Collections.Generic;
using UnityEngine;

public class PlayerNew : Player, IPlayer
{
    [Header("References")]
    [SerializeField] 

    private Dictionary<string, int> countBuildings;
}
