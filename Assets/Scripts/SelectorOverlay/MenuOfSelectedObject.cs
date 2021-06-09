using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOfSelectedObject : MonoBehaviour
{

    private IBuyer _buyer;

    #region Show/Hide
    public void Hide()
    {
    }

    public void Show(IBuyer buyer)
    {
        //_buyer = buyer;
        //transform.position = buyer.Position;

        //for (int i = 0; i < buyer.Purshases.Length; i++)
        //{
        //    actions[i].gameObject.SetActive(true);
        //    actions[i].sprite = buyer.Purshases[i].icon;
        //}

        //for (int i = buyer.Purshases.Length; i < maxActions; i++)  
        //{
        //    actions[i].gameObject.SetActive(false);
        //}

        //gameObject.SetActive(true);
    }
    #endregion
}
