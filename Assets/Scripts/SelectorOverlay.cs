using System.Collections.Generic;
using UnityEngine;

public class SelectorOverlay : MonoBehaviour
{
    public static SelectorOverlay Instance { get; private set; }

    [Header("Parameters")]
    [SerializeField] private int maxActions = 5;

    [Header("References")]
    [SerializeField] private List<SpriteRenderer> actions;

    private GameObject _selectedAction;

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
                
            }
        }
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
}
