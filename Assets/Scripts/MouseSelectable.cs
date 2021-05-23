using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectable : MonoBehaviour
{
    private IUnit _selectable;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out IUnit unit))
                {
                    if (_selectable != null && _selectable != unit) Deselect();

                    if (_selectable != unit)
                    {
                        _selectable = unit;
                        unit.Select();
                    }
                }
                else TryDeselect();
            }
            else TryDeselect();
        }
    }

    #region Deselect
    private void TryDeselect()
    {
        if (_selectable != null) Deselect();
    }

    private void Deselect()
    {
        _selectable.Deselect();
        _selectable = null;
    }
    #endregion
}
