using UnityEngine;

public class MouseSelectable : MonoBehaviour
{
    private ISelectable _selectable;

    private void Update()
    {
        TrySelect();
    }

    #region Select
    private void TrySelect()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out ISelectable selectable))
                {
                    if (selectable.NeedHidePrevios)
                    {
                        TryDeselect();
                        _selectable = selectable;
                    }
                    selectable.Select();
                }
                else TryDeselect();
            }
            else TryDeselect();
        }
    }
    #endregion

    #region Deselect
    private void TryDeselect()
    {
        if (_selectable != null)
        {
            _selectable.Deselect();
            _selectable = null;
        }
    }
    #endregion
}
