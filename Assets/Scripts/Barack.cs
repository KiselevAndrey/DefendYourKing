using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : MonoBehaviour, IBuilding
{

    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Select()
    {
        print(nameof(Select) + " " + gameObject.name);
    }

    public void Deselect()
    {
        print(nameof(Deselect) + " " + gameObject.name);
    }

    public void GetDamage(int damage)
    {
        throw new System.NotImplementedException();
    }


    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
