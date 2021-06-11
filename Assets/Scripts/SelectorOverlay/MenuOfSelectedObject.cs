using UnityEngine;

public class MenuOfSelectedObject : MonoBehaviour, ISeller
{
    //[Header("Parameters")]
    //[SerializeField] 

    [Header("References")]
    [SerializeField] private System.Collections.Generic.List<SelectedCell> cells;
    [SerializeField] private UnityEngine.UI.Text interpretationText;
    [SerializeField] private UnityEngine.UI.Text costText;
    [SerializeField] private SelectedCell buttonForBuy;
    [SerializeField] private Animator animator;

    private IBuyer _buyer;
    private Purchase _selectedPurchase;
    private bool _buyWhenClick;

    #region Start Update
    private void Start()
    {
        _buyWhenClick = !buttonForBuy.gameObject.activeSelf;

        Hide();
    }

    private void Update()
    {
        MouseUpperCell();
    }
    #endregion

    #region Show/Hide
    public void Hide()
    {
        animator.SetBool("Show", false);
    }

    public void Show(IBuyer buyer)
    {
        animator.SetBool("Show", true);

        if (_buyer == buyer) return;

        _buyer = buyer;

        for (int i = 0; i < buyer.Purchases.Count; i++)
        {
            cells[i].gameObject.SetActive(true);
            cells[i].SetIcon(buyer.Purchases[i].icon);
        }

        for (int i = buyer.Purchases.Count; i < cells.Count; i++)
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
        animator.SetBool("Show Interpretation", true);
        if (_selectedPurchase == purchase) return;

        _selectedPurchase = purchase;
        interpretationText.text = purchase.interpretation;
        costText.text = purchase.CalculateCost().ToString();
    }

    private void Deselect()
    {
        animator.SetBool("Show Interpretation", false);
    }
    #endregion

    #region Cell
    private void MouseUpperCell()
    {
        if (!_buyWhenClick) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out SelectedCell cell))
            {
                Select(_buyer.Purchases[cell.numberOfCell]);
            }
            else
                Deselect();
        }
        else
            Deselect();
    }

    public void ClickToCell(SelectedCell cell)
    {
        if (_buyWhenClick)
        {
            TryBuy();
        }
        else
        {
            Select(_buyer.Purchases[cell.numberOfCell]);
        }
    }
    #endregion
    #endregion

    public void TryBuy()
    {
        _buyer.TryBuy(_selectedPurchase, out string _negativeResult);
    }
}
