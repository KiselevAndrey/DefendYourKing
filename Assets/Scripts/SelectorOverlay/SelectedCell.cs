using UnityEngine;

public class SelectedCell : MonoBehaviour, ISelectable
{
    [Header("Parameters")]
    [Range(0, 5)] public int numberOfCell;

    [Header("References")]
    [SerializeField] private UnityEngine.UI.Image icon;

    [Header("Events")]
    [SerializeField] private UnityEngine.Events.UnityEvent OnClick;
    [SerializeField] private UnityEngine.Events.UnityEvent OnClickBy;

    #region Property
    public bool NeedHidePrevios => false;
    #endregion

    #region Deselect Select
    public void Deselect()
    {
        OnClickBy.Invoke();
    }

    public void Select()
    {
        OnClick.Invoke();
    }
    #endregion

    public void SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
    }
}
