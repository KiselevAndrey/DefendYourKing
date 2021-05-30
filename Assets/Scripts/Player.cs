using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        foreach (IUnit child in transform.GetComponentsInChildren<IUnit>())
        {
            child.Player = this;
        }
    }
}
