using UnityEngine;

public class MenuOfSelectedObject : MonoBehaviour, ISeller, ISelectable
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

    public bool NeedHidePrevios => false;

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
        DeselectCell();
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

        _selectedPurchase = purchase;
        UpdatePurchase(purchase);
    }

    private void UpdatePurchase(Purchase purchase)
    {
        interpretationText.text = purchase.interpretation;
        costText.text = purchase.CalculateCost().ToString();
    }
    #endregion

    #region GUI Cell
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
                DeselectCell();
        }
        else
            DeselectCell();
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

    public void DeselectCell()
    {
        animator.SetBool("Show Interpretation", false);
    }
    #endregion
    #endregion

    public void TryBuy()
    {
        if (_buyer.TryBuy(_selectedPurchase, out string _negativeResult))
            UpdatePurchase(_selectedPurchase);
    }

    public void Select()
    {
        DeselectCell();
    }

    public void Deselect()
    {
    }
}
