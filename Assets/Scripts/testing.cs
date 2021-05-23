using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    [SerializeField] private IUnit selectable;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out IUnit unit))
            {
                if (selectable != null && selectable == unit) Deselect();

                //if (selectable != unit)
                //{
                    selectable = unit;
                    unit.Select();
                //}
            }
            else TryDeselect();
        }
        else TryDeselect();

    }

    private void TryDeselect()
    {
        if (selectable != null) Deselect();
    }
    private void Deselect()
    {
        selectable.Deselect();
        selectable = null;
    }
}
