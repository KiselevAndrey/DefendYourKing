using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorOverlay : MonoBehaviour, ISeller
{
    public static SelectorOverlay Instance { get; private set; }

    [Header("Parameters")]
    [SerializeField] private int maxActions = 5;

    [Header("References")]
    [SerializeField] private List<SpriteRenderer> actions;
    [SerializeField] private Image interpretationBackground;
    [SerializeField] private Text interpretationText;

    private Purchase _selectedAction;
    private IBuyer _buyer;

    #region Awake Update
    private void Awake()
    {
        Instance = this;
        Hide();
        interpretationBackground.gameObject.SetActive(false);
    }

    private void Update()
    {
        MouseUpperAction();
        CheckClickToAction();
    }
    #endregion

    #region Show/Hide
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(IBuyer buyer)
    {
        _buyer = buyer;
        transform.position = buyer.Position;

        for (int i = 0; i < buyer.Purshases.Length; i++)
        {
            actions[i].gameObject.SetActive(true);
            actions[i].sprite = buyer.Purshases[i].icon;
        }

        for (int i = buyer.Purshases.Length; i < maxActions; i++)
        {
            actions[i].gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }
    #endregion

    #region Select Deselect Action
    private void MouseUpperAction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Action"))
            {
                int numberAction = int.Parse(hit.collider.gameObject.name[0].ToString());
                Select(_buyer.Purshases[numberAction]);
            }
            else
                Deselect();
        }
        else
            Deselect();

    }

    private void CheckClickToAction()
    {
        if(Input.GetMouseButtonUp(0) && _selectedAction != null)
        {
            _buyer.TryBuy(_selectedAction);
        }
    }

    private void Select(Purchase selected)
    {
        Deselect();

        _selectedAction = selected;

        interpretationBackground.gameObject.SetActive(true);
        interpretationBackground.sprite = selected.interpretationBackground;
        interpretationText.text = selected.interpretation;
    }

    private void Deselect()
    {
        if(_selectedAction != null)
        {
            interpretationBackground.gameObject.SetActive(false);
            _selectedAction = null;
        }
    }
    #endregion
}
