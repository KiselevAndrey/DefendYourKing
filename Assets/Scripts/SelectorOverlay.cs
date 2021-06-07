using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorOverlay : MonoBehaviour
{
    public static SelectorOverlay Instance { get; private set; }

    [Header("Parameters")]
    [SerializeField] private int maxActions = 5;

    [Header("References")]
    [SerializeField] private List<SpriteRenderer> actions;
    [SerializeField] private Image interpretation;

    private Purshase _selectedAction;
    private IBuyer _buyer;

    #region Awake Update
    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void Update()
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
    #endregion

    #region Show/Hide
    public void Show(IBuilding building)
    {
        transform.position = building.Position;

        gameObject.SetActive(true);
    }

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

    #region Select Deselect
    private void Select(Purshase selected)
    {
        Deselect();

        _selectedAction = selected;

        interpretation.enabled = true;
        interpretation.sprite = selected.interpretation;
    }

    private void Deselect()
    {
        if(_selectedAction != null)
        {
            interpretation.enabled = false;
            _selectedAction = null;
        }
    }
    #endregion
}
