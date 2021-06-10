using UnityEngine;

public class MenuOfSelectedObject : MonoBehaviour, ISeller
{
    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<SelectedCell> cells;
    [SerializeField] private UnityEngine.UI.Text interpretationText;

    private IBuyer _buyer;

    #region Show/Hide
    public void Hide()
    {
    }

    public void Show(IBuyer buyer)
    {
        if (_buyer == buyer) return;

        _buyer = buyer;

        for (int i = 0; i < buyer.Purshases.Length; i++)
        {
            cells[i].gameObject.SetActive(true);
            cells[i].SetIcon(buyer.Purshases[i].icon);
        }

        for (int i = buyer.Purshases.Length; i < cells.Count; i++)
        {
            cells[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }
    #endregion


    #region Select Deselect
    #region Purchases
    private void Select(Purchase purchase)
    {

    }

    private void Deselect()
    {

    }
    #endregion

    #region Cell
    private void MouseUpperCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out SelectedCell cell))
            {
                Select(_buyer.Purshases[cell.numberOfCell]);
            }
            else
                Deselect();
        }
        else
            Deselect();
    }

    public void ClickToCell(int numCell)
    {
        print(numCell);
    }

    public void ClickToCell(SelectedCell cell)
    {
        print(cell);
    }
    #endregion
    #endregion
}
