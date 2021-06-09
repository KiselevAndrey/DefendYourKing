using UnityEngine;

public class MouseSelectable : MonoBehaviour
{
    private ISelectable _selectable;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out ISelectable selectable))
                {
                    if (selectable.SelectAndDeselectPrevious())
                    {
                        TryDeselect();
                        _selectable = selectable;
                        print(_selectable);
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
        if (_selectable != null)
        {
            _selectable.Deselect();
            _selectable = null;
        }
    }
    #endregion
}
